mergeInto(LibraryManager.library, {

  Auth: function() {
    auth();
  },

  GetData: function() {
    getUserData();
  },
  
  SetData : function(data){
    setUserData(UTF8ToString(data));
  },

  ShowCommonADV: function () {
    showFullscrenAd();
  },

  ShowRewardADV: function() {
    showRewardedAd();
  },
  
  GetLeaderBoardEntries: function(){
    GetLeaderBoardEntries();
  },
  
  SetLeaderBoard: function(score){
    SetLeaderBoard(score);
  }

});