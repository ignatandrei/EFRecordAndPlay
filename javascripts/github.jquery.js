// http://aboutcode.net/2010/11/11/list-github-projects-using-javascript.html
 
jQuery.githubUser = function(username, callback) {
  //jQuery.getJSON("http://github.com/api/v1/json/" + username + "?callback=?", callback);
  jQuery.getJSON("https://api.github.com/users/" + username + "/repos?callback=?", callback);
}
 
jQuery.fn.loadRepositories = function(username) {
  this.html("<span>Querying GitHub for repositories...</span>");

  var target = this; 
  $.githubUser(username, function(data) {
   debugger;
    var repos = data.data;
    //sortByNumberOfWatchers(repos);
 
    var list = $('<div />');
    target.empty().append(list);
    $(repos).each(function() {
     if(!this.fork){
      
       list.append('<h1>'+ this.name +'</h1>');
        list.append('<p>' + this.description + '</p>')

        list.append('<p class="view"><a href='+ this.url +'>View the Project on GitHub <small>'+ this.full_name +'</small></a></p>');

      
     }
     });
  });
 
  function sortByNumberOfWatchers(repos) {
    repos.sort(function(a,b) {
      return b.watchers_count - a.watchers_count;
    });
  }
};
