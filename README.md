# DestinationBucketList

Every time you add a new functionality make sure you do this:
```
git fetch
git pull origin master
git merge master
```

and then create a new pull request.

Every pull request should have FE or BE in its title based on where the feature was added(frontend/backend).

Please avoid creating a pull request that contains changes both on backend and frontend. Each pull request should have changes only on one end.

If you want to begin working on a new feature pull all changes from remote using
```
git checkout master
git fetch
git pull origin master
git checkout -b [feature_name]
```

and start working.

Note:    
If you work on backend and you add/modify a model please do the migrations prior to pushing the code.  



<span style="color: red">!!! Never push to master branch, we will work with pull requests to avoid conflicts. !!! </span>


