# bin

## jgit
Requirements: peco, jq

### clone
Clone repository and add upstream (parent repository) automatically.

``` sh
# set environment variables
# example
GITHUB_ME="jyane"
GITHUB_FQDN="github.com"
GITHUB_URL="https://${GITHUB_FQDN}"
GITHUB_API_URL="https://api.${GITHUB_FQDN}"

# git clone https://github.com/jyane/bin
# cd ${HOME}/repos/github.com/jyane/bin
# git remote add upstream https://github.com/jyane/bin
jgit clone bin

# git clone https://github.com/jyane/bin
# cd ${HOME}/repos/github.com/jyane/bin
# git remote add upstream https://github.com/grpc/grpc
jgit clone jyane/grpc
```

### fpush
Upsteam safe force pushing.
Cannot push to master branch through this command.

``` sh
# force push to current branch
# git push -f "origin" ${current_branch}
jgit fpush
```

### sync
Pull upstream changes and push to origin.

``` sh
jgit sync
```

### pr
Fetch a pull request changes and checkout its pull request branch.

```
jgit pr 42
```
