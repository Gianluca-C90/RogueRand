using AlgoSdk;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System.Runtime.InteropServices;

/// <summary>
/// Enum to manage different wallet types
/// </summary>
enum WalletType
{
    MYALGOWALLET,
    ALGOSIGNER,
    ALGOWALLET
}

public class AlgoServer : MonoBehaviour
{
    #region DllImport

    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern string ConnectMyAlgo();

    [DllImport("__Internal")]
    private static extern string ConnectAlgoSign();

    [DllImport("__Internal")]
    private static extern void Tsx(WalletType type, string sender, string receiver, float amount, string note);

    [DllImport("__Internal")]
    private static extern void Optin(WalletType type, string sender, string asa, string note);

    [DllImport("__Internal")]
    private static extern void AsaTxn(WalletType type, string sender, string receiver, string asa, float amount, string note);

    #endregion

    AlgodClient algod;
    string clientAddr;
    WalletType type;
    bool holdASA;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        algod = new AlgodClient(
            address: "http://localhost:4001",
            token: "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
        );
        CheckAlgodStatus().Forget();
        CheckBalance().Forget();
    }

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            var uniTaskVoid = checkASA(47482804);
        }
    }

    public void GetConnAlgoSign()
    {
        ConnectAlgoSign();
        type = WalletType.ALGOSIGNER;
    }

    public void GetConnMyAlgo()
    {
        ConnectMyAlgo();
        type = WalletType.MYALGOWALLET;
    }

    public void GetAddr(string rec)
    {
        clientAddr = rec;
    }

    public void SendASA()
    {
        var privateKey = Mnemonic
            .FromString("shoe globe wild feed friend level citizen hotel person scrub riot van lottery degree wild math demand guide meat rug capable bamboo fiber absorb elephant")
            .ToPrivateKey();
        UniTaskVoid uniTaskVoid = SendAsset(privateKey, clientAddr, 47482804, 1);
    }

    public async UniTaskVoid MakePayment(PrivateKey privateKey, Address receiver, ulong amount)
    {
        // Get the secret key handle and the public key of the sender account.
        // We'll use the secret key handle to sign the transaction.
        // The public key will be used as the sender's Address.
        using var keyPair = privateKey.ToKeyPair();

        // Get the suggested transaction params
        var (txnParamsError, txnParams) = await algod.GetSuggestedParams();
        if (txnParamsError.IsError)
        {
            Debug.LogError(txnParamsError.Message);
            return;
        }


        // Construct and sign the payment transaction
        var paymentTxn = Transaction.Payment(
            sender: keyPair.PublicKey,
            txnParams: txnParams,
            receiver: receiver,
            amount: amount
        );
        var signedTxn = paymentTxn.Sign(keyPair.SecretKey);

        // Send the transaction
        var (sendTxnError, txid) = await algod.SendTransaction(signedTxn);
        if (sendTxnError.IsError)
        {
            Debug.LogError(sendTxnError.Message);
            return;
        }

        // Wait for the transaction to be confirmed
        PendingTransaction pending = default;
        ErrorResponse error = default;
        while (pending.ConfirmedRound == 0)
        {
            (error, pending) = await algod.GetPendingTransaction(txid);
            if (error.IsError)
            {
                Debug.LogError(error.Message);
                return;
            }
            await UniTask.Delay(1000);
        }

        Debug.Log(pending);
        Debug.Log($"Successfully made payment! Confirmed on round {pending.ConfirmedRound}");
    }

    public async UniTaskVoid CreateAsset(PrivateKey privateKey, Address receiver, ulong amount)
    {
        // Get the secret key handle and the public key of the sender account.
        // We'll use the secret key handle to sign the transaction.
        // The public key will be used as the sender's Address.
        using var keyPair = privateKey.ToKeyPair();

        // Get the suggested transaction params
        var (txnParamsError, txnParams) = await algod.GetSuggestedParams();
        if (txnParamsError.IsError)
        {
            Debug.LogError(txnParamsError.Message);
            return;
        }

        var asset = new AssetParams();
        asset.Name = "Dio";
        asset.UnitName = "DIO";
        asset.Creator = keyPair.PublicKey;
        asset.Decimals = 0;
        asset.Manager = keyPair.PublicKey;
        asset.Reserve = keyPair.PublicKey;
        asset.Total = 10;

        var assetTxn = Transaction.AssetCreate(
            sender: keyPair.PublicKey,
            txnParams: txnParams,
            assetParams: asset
        );

        var signedTxn = assetTxn.Sign(keyPair.SecretKey);

        // Send the transaction
        var (sendTxnError, txid) = await algod.SendTransaction(signedTxn);
        if (sendTxnError.IsError)
        {
            Debug.LogError(sendTxnError.Message);
            return;
        }

        // Wait for the transaction to be confirmed
        PendingTransaction pending = default;
        ErrorResponse error = default;
        while (pending.ConfirmedRound == 0)
        {
            (error, pending) = await algod.GetPendingTransaction(txid);
            if (error.IsError)
            {
                Debug.LogError(error.Message);
                return;
            }
            await UniTask.Delay(1000);
        }

        Debug.Log($"Successfully created asset! Confirmed on round {pending.ConfirmedRound}");
    }

    public async UniTaskVoid SendAsset(PrivateKey privateKey, Address receiver, ulong xferAsset, ulong amount)
    {
        // Get the secret key handle and the public key of the sender account.
        // We'll use the secret key handle to sign the transaction.
        // The public key will be used as the sender's Address.
        using var keyPair = privateKey.ToKeyPair();

        // Get the suggested transaction params
        var (txnParamsError, txnParams) = await algod.GetSuggestedParams();
        if (txnParamsError.IsError)
        {
            Debug.LogError(txnParamsError.Message);
            return;
        }

        var (txnInfosError, txnInfos) = await algod.GetAccountInformation(receiver);
        if (txnInfosError.IsError)
        {
            Debug.LogError(txnParamsError.Message);
            return;
        }

        var assetTxn = Transaction.AssetTransfer(
                sender: keyPair.PublicKey,
                txnParams: txnParams,
                xferAsset: xferAsset,
                assetAmount: amount,
                assetReceiver: receiver
            );

        Debug.Log($"Sending asset with ID: {xferAsset}...");

        var signedTxn = assetTxn.Sign(keyPair.SecretKey);

        // Send the transaction
        var (sendTxnError, txid) = await algod.SendTransaction(signedTxn);
        if (sendTxnError.IsError)
        {
            Debug.LogError(sendTxnError.Message);
            return;
        }

        // Wait for the transaction to be confirmed
        PendingTransaction pending = default;
        ErrorResponse error = default;
        while (pending.ConfirmedRound == 0)
        {
            (error, pending) = await algod.GetPendingTransaction(txid);
            if (error.IsError)
            {
                Debug.LogError(error.Message);
                return;
            }
            await UniTask.Delay(1000);
        }

        Debug.Log($"Successfully sent asset! Confirmed on round {pending.ConfirmedRound}");
    }

    public async UniTaskVoid CheckAlgodStatus()
    {
        var response = await algod.GetHealth();
        if (response.Error.IsError)
        {
            Debug.LogError(response.Error.Message);
        }
        else
        {
            Debug.Log("Connected to algod!");
        }
    }

    public async UniTaskVoid CheckBalance()
    {
        var accountAddress = "HIEF2R3JQGNPGOMMUC6ZGLYGUBXVY2KBFGF5DOLTBMXLOC2FAIAM6666FY";
        var (error, accountInfo) = await algod.GetAccountInformation(accountAddress);
        if (error.IsError)
        {
            Debug.LogError(error.Message);
        }
        else
        {
            Debug.Log($"My account has {accountInfo.Amount / 1_000_000} algos");
        }
    }

    private async UniTaskVoid checkASA(ulong asset_id)
    {
        var (error, accountInfo) = await algod.GetAccountInformation(clientAddr);
        if (error.IsError)
        {
            Debug.LogError(error.Message);
        }
        else
        {
            var idx = 0;
            foreach (var info in accountInfo.Assets)
            {
                var scrutinized_asset = accountInfo.Assets[idx];
                idx++;
                if (scrutinized_asset.AssetId == asset_id)
                {
                    holdASA = true;
                    Debug.Log($"Asset with ID={asset_id} already registred");
                    SendASA();
                    return;
                }
            }
            Optin(type, clientAddr, asset_id.ToString(), "");

        }
    }
}