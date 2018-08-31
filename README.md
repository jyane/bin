# bin

## jpb

```
~/bin] (shohei_kamimori@skamimori 2018-08-31 18:11:32)
-> echo 'hoge' | pbcopy
[~/bin] (shohei_kamimori@skamimori 2018-08-31 18:11:44)
-> jpb sed 's/o//g'
[~/bin] (shohei_kamimori@skamimori 2018-08-31 18:11:47)
-> pbpaste
hge
```

## jswap
```
echo 'a b' | jswap # b a
```

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
