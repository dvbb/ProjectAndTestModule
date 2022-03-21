# git most common command

> reference: https://segmentfault.com/a/1190000039147662

## 名词

>工作区: Workspace  
暂存区: Index / Stage  
本地仓库:  Repository  
远程仓库: Remote


## 流程
1. 克隆所需的远程仓库
2. `git checkout [BranchName]` 切换到工作的分支
3. `git fetch` 下载最新的变动
4. `git pull` 拉取最新的变动
5. 本地进行开发
6. `git add` 将工作区文件提交到暂存区
7. `git commit`将暂存区内的所有文件提交到本地仓库
8. `git push`将本地仓库的变动提交到远程仓库


## Command
### 增加/删除文件
``` powershell
# 添加指定文件到暂存区
$ git add [file1] [file2] ...
# 添加所有文件到暂存区
$ git add *
# 添加指定目录到暂存区，包括子目录
$ git add [dir]
# 添加当前目录的所有文件到暂存区
$ git add .
# 删除工作区文件，并且将这次删除放入暂存区
$ git rm [file1] [file2] ...
# 停止追踪指定文件，但该文件会保留在工作区
$ git rm --cached [file]
# 改名文件，并且将这个改名放入暂存区
$ git mv [file-original] [file-renamed]
```

### 代码提交
``` powershell
# 提交暂存区到仓库区
$ git commit -m [message]
# 提交暂存区的指定文件到仓库区
$ git commit [file1] [file2] ... -m [message]
# 提交工作区自上次commit之后的变化，直接到仓库区
$ git commit -a
# 提交时显示所有diff信息
$ git commit -v
# 将add和commit合为一步
$ git commit -am 'message'
# 使用一次新的commit，替代上一次提交
# 如果代码没有任何新变化，则用来改写上一次commit的提交信息
$ git commit --amend -m [message]
# 重做上一次commit，并包括指定文件的新变化
$ git commit --amend [file1] [file2] ...
```

### 分支
``` powershell
# 列出所有本地分支
$ git branch
# 列出所有远程分支
$ git branch -r
# 列出所有本地分支和远程分支
$ git branch -a
# 新建一个分支，但依然停留在当前分支
$ git branch [branch-name]
# 新建一个分支，并切换到该分支
$ git checkout -b [branch]
# 新建一个分支，指向指定commit
$ git branch [branch] [commit]
# 新建一个分支，与指定的远程分支建立追踪关系
$ git branch --track [branch] [remote-branch]
# 切换到指定分支，并更新工作区
$ git checkout [branch-name]
# 切换到上一个分支
$ git checkout -
# 建立追踪关系，在现有分支与指定的远程分支之间
$ git branch --set-upstream [branch] [remote-branch]
# 合并指定分支到当前分支
$ git merge [branch]
# 选择一个commit，合并进当前分支
$ git cherry-pick [commit]
# 删除分支
$ git branch -d [branch-name]
# 删除远程分支
$ git push origin --delete [branch-name]
$ git branch -dr [remote/branch]
# 检出版本v2.0
$ git checkout v2.0
# 从远程分支develop创建新本地分支devel并检出
$ git checkout -b devel origin/develop
# 检出head版本的README文件（可用于修改错误回退）
git checkout -- README 
```

### 查看信息
``` powershell
# 显示有变更的文件
$ git status
# 显示当前分支的版本历史
$ git log
# 显示commit历史，以及每次commit发生变更的文件
$ git log --stat
# 搜索提交历史，根据关键词
$ git log -S [keyword]
# 显示某个commit之后的所有变动，每个commit占据一行
$ git log [tag] HEAD --pretty=format:%s
# 显示某个commit之后的所有变动，其"提交说明"必须符合搜索条件
$ git log [tag] HEAD --grep feature
# 显示某个文件的版本历史，包括文件改名
$ git log --follow [file]
$ git whatchanged [file]
# 显示过去5次提交
$ git log -5 --pretty --oneline
$ git log -i head~5
# 显示当前分支的最近几次提交
$ git reflog
```

### 远程同步
``` powershell
# 下载远程仓库的所有变动
$ git fetch 
# 拉取远程仓库的变动
$ git pull 
# 上传本地分支到远程仓库
$ git push 
# 下载远程仓库的所有变动
$ git fetch [remote]
# 显示某个远程仓库的信息
$ git remote show [remote]
# 增加一个新的远程仓库，并命名
$ git remote add [shortname] [url]
# 取回远程仓库的变化，并与本地分支合并
$ git pull [remote] [branch]
# 上传本地指定分支到远程仓库
$ git push [remote] [branch]
# 强行推送当前分支到远程仓库，即使有冲突
$ git push [remote] --force
# 推送所有分支到远程仓库
$ git push [remote] --all
```

### 撤销
``` powershell
# 撤销某一次提交的变动
$ git revert [commit]
```
