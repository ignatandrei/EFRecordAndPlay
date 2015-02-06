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
 
    var list = $('<dl/>');
    target.empty().append(list);
    $(repos).each(function() {
      list.append('<dt><a href="'+ this.url +'">' + this.name + '</a></dt>');
      list.append('<dd>' + this.description + '</dd>');
    });
  });
 
  function sortByNumberOfWatchers(repos) {
    repos.sort(function(a,b) {
      return b.watchers_count - a.watchers_count;
    });
  }
};
