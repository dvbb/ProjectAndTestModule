# rebase command
##### 作用
* 可以对某一段线性提交历史进行编辑、删除
##### 指令
* git rebase -i  [startpoint]  [endpoint]
> -i(--interactive): 弹出交互式的界面让用户编辑完成合并操作  
[startpoint]: 指定了一个编辑区的起点  
[endpoint]: 默认是当前分支HEAD所指向的commit  
* git rebase -i 299d113
* git rebase -i Head~5

##### sample

``` powershell
$ ls
firstgit.txt second.txt  third.txt
```
将三个txt文件分别`git add [file]`, `git commit -m "[ordinal] commit"`

可以使用`git log`指令查看提交记录
``` powershell
$ git log
commit 7d94bec554e278ae449a41c2f3355b12573243ed (HEAD -> main)
Author: dvbb
Date:   Thu Mar 17 14:49:17 2022 +0800

    third commit

commit e273fa464532283a13662dd5c783e7ab91edde56
Author: dvbb
Date:   Thu Mar 17 14:49:02 2022 +0800

    second commit

commit b9a4dc54a6d1d8d80584b1ef8214542fa02202d7
Author: dvbb
Date:   Thu Mar 17 14:48:40 2022 +0800

    first commit

commit ca14c3a5da6997bf4bd69e710ee1dde690a1dfe1 (origin/main, origin/HEAD)
Author: dvbb
Date:   Tue Mar 8 13:56:44 2022 +0800

    Create HashtableDoc.ps1

```

使用`rebase`将前三个记录合并(下方两个指令效果相同)
* git rebase -i ca14c3a5da6997bf4bd69e710ee1dde690a1dfe1
* git rebase -i head~3
``` powershell
pick b9a4dc5 first commit
pick e273fa4 second commit
pick 7d94bec third commit

# Rebase ca14c3a..7d94bec onto ca14c3a (3 commands)
#
# Commands:
# p, pick <commit> = use commit
# r, reword <commit> = use commit, but edit the commit message
# e, edit <commit> = use commit, but stop for amending
# s, squash <commit> = use commit, but meld into previous commit
# f, fixup [-C | -c] <commit> = like "squash" but keep only the previous
#                    commit's log message, unless -C is used, in which case
#                    keep only this commit's message; -c is same as -C but
#                    opens the editor
# x, exec <command> = run command (the rest of the line) using shell
# b, break = stop here (continue rebase later with 'git rebase --continue')
# d, drop <commit> = remove commit
# l, label <label> = label current HEAD with a name
# t, reset <label> = reset HEAD to a label
# m, merge [-C <commit> | -c <commit>] <label> [# <oneline>]
# .       create a merge commit using the original merge commit's
# .       message (or the oneline, if no original merge commit was
# .       specified); use -c <commit> to reword the commit message
#
# These lines can be re-ordered; they are executed from top to bottom.
#
# If you remove a line here THAT COMMIT WILL BE LOST.
#
# However, if you remove everything, the rebase will be aborted.

```

> 指令说明：
pick：保留该commit（缩写:p）  
reword：保留该commit，但我需要修改该commit的注释（缩写:r）  
edit：保留该commit, 但我要停下来修改该提交(不仅仅修改注释)（缩写:e）  
squash：将该commit和前一个commit合并（缩写:s）  
fixup：将该commit和前一个commit合并，但我不要保留该提交的注释信息（缩写:f）  
exec：执行shell命令（缩写:x）  
drop：我要丢弃该commit（缩写:d）  

VIM界面I键进入
修改为：
``` powershell
r b9a4dc5 commit all txt
s e273fa4 second commit
s 7d94bec third commit
```

`ESC` + `:wq` 退出vim编辑器

同理修改Commit名字、描述
此时rebase完成。

##### 推送到远程仓库
若rebase了远程仓库中存在的commit，则无法直接push到remote(因为远程仓库存在本地没有的commit，本地版本落后)
可使用force push修改远程仓库的提交记录
``` powershell
& git push --force
```