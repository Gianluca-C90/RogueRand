function helloworldbitch()
{
    window.alert("CONNECT YOUR WALLET TO START GAME");
}

//#region MyAlgoWallet

/*Warning: Browser will block pop-up if user doesn't trigger myAlgoWallet.connect() with a button interation */
async function connectToMyAlgo() {
    try {
        const myAlgoWallet = new MyAlgoConnect();
        const accounts = await myAlgoWallet.connect();
        const addresses = accounts.map(account => account.address);
        let rec = addresses.toString();
        console.log(rec);
        unityInstance.SendMessage('Communication', 'GetAddr', rec);
        window.alert("ARE YOU READY TO DIE?");
    } catch (err) {
        console.error(err);
    }
}

async function makeTxn_MyA(sender, receiver, amount, note)
{

    try {
        const algodClient = new algosdk.Algodv2("", 'https://api.testnet.algoexplorer.io', '');
        const params = await algodClient.getTransactionParams().do();

        console.log(params);

        const txn = algosdk.makePaymentTxnWithSuggestedParamsFromObject({
            suggestedParams: params,
            from: sender,
            to: receiver, 
            amount: amount,
            note: note
        });

        console.log(txn);

        const myAlgoConnect = new MyAlgoConnect();
        const signedTxn = await myAlgoConnect.signTransaction(txn.toByte());

        console.log(signedTxn);

        const response = await algodClient.sendRawTransaction(signedTxn.blob).do();
    } catch (error) {
        console.error(err);
    }
}

async function optinASA_MyA(asaID, sender, note)
{
    try {
        const algodClient = new algosdk.Algodv2("", 'https://api.testnet.algoexplorer.io', '');
        const params = await algodClient.getTransactionParams().do();

        const txn = algosdk.makeAssetTransferTxnWithSuggestedParamsFromObject({
            from: sender,
            to: sender,
            assetIndex: +asaID,
            amount: 0,
            suggestedParams: {...params}
          });

        const myAlgoConnect = new MyAlgoConnect();
        const signedTxn = await myAlgoConnect.signTransaction(txn.toByte());

        const response = await algodClient.sendRawTransaction(signedTxn.blob).do();

        unityInstance.SendMessage('Communication', 'SendASA');

    } catch (error) {
        console.error(err);
    }
}
/*
async function sendASA_MyA(sender, receiver, asaID, amount, note)
{
    try {
        const algodClient = new algosdk.Algodv2("", 'https://api.testnet.algoexplorer.io', '');
        const params = await algodClient.getTransactionParams().do();

        const txn = algosdk.makeAssetTransferTxnWithSuggestedParamsFromObject({
            suggestedParams: params,
            from: sender,
            to: receiver,
            appIndex: asaID,
            amount: amount,
            note: note
        });

        const myAlgoConnect = new MyAlgoConnect();
        const signedTxn = await myAlgoConnect.signTransaction(txn.toByte());

        const response = await algodClient.sendRawTransaction(signedTxn.blob).do();

    } catch (error) {
        console.error(err);
    }
}*/

//#endregion

//#region AlgoSigner

const algodServer = 'https://testnet-algorand.api.purestake.io/ps2'
const indexerServer = 'https://testnet-algorand.api.purestake.io/idx2'
const token = { 'X-API-Key': 'EE26hc24gksBM8jgQ6p5966wxp7QvFASCI6zEW60' }
const port = '';

let algodClient = {};
let indexerClient = {};

async function connectToAlgoSigner()
{
    if(typeof AlgoSigner !== 'undefined') {
        // connects to the browser AlgoSigner instance
        AlgoSigner.connect()
            // finds the TestNet accounts currently in AlgoSigner
            .then(() => AlgoSigner.accounts({
                ledger: 'TestNet'
            }))
            .then((accountData) => {
                // the accountData object should contain the Algorand addresses from TestNet that AlgoSigner currently knows about
                console.log(accountData[0].address);
                unityInstance.SendMessage('Communication', 'GetAddr', accountData[0].address);
            })
            .then(() => {
                algodClient = new algosdk.Algodv2(token, algodServer, port);
                indexerClient = new algosdk.Indexer(token, indexerServer, port);

                algodClient.healthCheck().do();
            })
            .then(() => {
                console.log("Healthy");
                window.alert("ARE YOU READY TO DIE?");

            })
            .catch((e) => {
                // handle errors and perform error cleanup here
                console.error(e);
            })
    }
}

async function makeTxn_AS(sender, receiver, am, nt)
{
    let from = sender; 
    let to = receiver; 
    let amount = am; 
    let note = nt; 

    AlgoSigner.connect()
        .then(() => algodClient.getTransactionParams().do())
        then((txParamsJS) => 
        {
            const txn = algosdk.makeAssetTransferTxnWithSuggestedParamsFromObject({
                from: from,
                to: to,
                amount: amount,
                note: AlgoSigner.encoding.stringToByteArray(note),
                suggestedParams: {...txParamsJS}
              });
              
            // Use the AlgoSigner encoding library to make the transactions base64
            const txn_b64 = AlgoSigner.encoding.msgpackToBase64(txn.toByte());
            AlgoSigner.signTxn([{txn: txn_b64}])
            .then((signedTxs) => AlgoSigner.send({
                ledger: 'TestNet',
                tx: signedTxs[0].blob
              }))
            .then((tx) => console.log(tx))
        })
        .catch((e) => { 
            // handle errors and perform error cleanup here
            console.error(e); 
        });
}

async function optinASA_AS(asaID, receiver, nt)
{
    let assetId = asaID;
    let optInAccount = receiver; 
    let note = nt; 

    

    AlgoSigner.connect()
        .then(() => algodClient.getTransactionParams().do())
        .then((txParamsJS) => 
        {
            const txn = algosdk.makeAssetTransferTxnWithSuggestedParamsFromObject({
                from: optInAccount,
                to: optInAccount,
                assetIndex: +assetId,
                amount: 0,
                note: AlgoSigner.encoding.stringToByteArray(note),
                suggestedParams: {...txParamsJS}
              });
              
            // Use the AlgoSigner encoding library to make the transactions base64
            const txn_b64 = AlgoSigner.encoding.msgpackToBase64(txn.toByte());
            AlgoSigner.signTxn([{txn: txn_b64}])
            .then((signedTxs) => AlgoSigner.send({
                ledger: 'TestNet',
                tx: signedTxs[0].blob
              }))
            .then((tx) => waitForAlgosignerConfirmation(tx)) 
            .then(() => {
                // our transaction was successful, we can now view it on the blockchain 
                unityInstance.SendMessage('Communication', 'SendASA');
            })
        })
        .catch((e) => 
        { 
            // handle errors and perform error cleanup here
            console.error(e); 
        });
}

/*
async function sendASA_AS(asaID, sender, receiver, am, nt)
{
    let assetId = asaID;
    let from = sender; 
    let to = receiver; 
    let amount = am; 
    let note = nt; 

    AlgoSigner.connect()
        .then(() => algodClient.getTransactionParams().do())
        .then((txParamsJS) => 
        {
            const txn = algosdk.makeAssetTransferTxnWithSuggestedParamsFromObject({
                from: from,
                to: to,
                assetIndex: +assetId,
                amount: amount,
                note: AlgoSigner.encoding.stringToByteArray(note),
                suggestedParams: {...txParamsJS}
              });
              
            // Use the AlgoSigner encoding library to make the transactions base64
            const txn_b64 = AlgoSigner.encoding.msgpackToBase64(txn.toByte());
            AlgoSigner.signTxn([{txn: txn_b64}])
            .then((signedTxs) => AlgoSigner.send({
                ledger: 'TestNet',
                tx: signedTxs[0].blob
              }))
            .then((tx) => waitForAlgosignerConfirmation(tx)) 
            .then(() => {
                // our transaction was successful, we can now view it on the blockchain 
                console.log("asset inviato");
            })
        })
        .catch((e) => 
        { 
            // handle errors and perform error cleanup here
            console.error(e); 
        });
}*/

async function waitForAlgosignerConfirmation(tx) {
    console.log(`Transaction ${tx.txId} waiting for confirmation...`);
    let status = await AlgoSigner.algod({
      ledger: 'TestNet',
      path: '/v2/transactions/pending/' + tx.txId
    });
  
    while(true) {
      if(status['confirmed-round'] !== null && status['confirmed-round'] > 0) {
        //Got the completed Transaction
        console.log(`Transaction confirmed in round ${status['confirmed-round']}.`);
        break;
      }
  
      status = await AlgoSigner.algod({
        ledger: 'TestNet',
        path: '/v2/transactions/pending/' + tx.txId
      });
    }
    
    return tx;
  }

//#endregion