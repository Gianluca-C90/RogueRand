var myLibrary = {
    Hello: function()
    {
        helloworldbitch();
    },

    ConnectMyAlgo: function()
    {
        connectToMyAlgo();
    },

    ConnectAlgoSign: function()
    {
        connectToAlgoSigner();
    },

    Tsx: function(type, sender, receiver, amount, note)
    {
        var s = Pointer_stringify(sender);
        var r = Pointer_stringify(receiver);
        var n = Pointer_stringify(note);

        switch(type)
        {
            case 0:
                makeTxn_MyA(s, r, amount, n);
                break;
            case 1:
                makeTxn_AS(s, r, amount, n);
                break;
            case 2:
                //TODO: Aggiungere AlgoWalletOfficial
        };
    },

    Optin: function(type, sender, asa, am, note)
    {
        var s = Pointer_stringify(sender);
        var a = Pointer_stringify(asa);
        var n = Pointer_stringify(note);
        var m = Pointer_stringify(am);

        switch(type)
        {
            case 0:
                optinASA_MyA(a, s, m, n);
                break;
            case 1:
                optinASA_AS(a, s, m, n);
                break;
            case 2:
                //TODO: Aggiungere AlgoWalletOfficial
        };
        
    },
/*
    AsaTxn: function(sender, receiver, asa, amount, note)
    {
        var s = Pointer_stringify(sender);
        var r = Pointer_stringify(receiver);
        var a = Pointer_stringify(asa);
        var n = Pointer_stringify(note);

        switch(type)
        {
            case 0:
                sendASA_MyA(s, r, a, amount, n);
                break;
            case 1:
                sendASA_AS(s, r, a, amount, n);
                break;
            case 2:
                //TODO: Aggiungere AlgoWalletOfficial
        };
    }*/
};

mergeInto(LibraryManager.library, myLibrary)