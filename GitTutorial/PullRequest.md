# Pull Request
> ref: http://www.ruanyifeng.com/blog/2017/07/pull_request.html
## Definition
[官方定义](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/proposing-changes-to-your-work-with-pull-requests/about-pull-requests)中Pull Request 是一种通知机制。你修改了他人的代码，将你的修改通知原来的作者，希望他合并你的修改，这就是 Pull Request。

## Benefits
在PR页面，可以触发CI，测试该次改动有没有合并的资格。同时工作组的其他人也可以看到本次PR的代码改动，并讨论、审核和修改代码。

## Process
1. 在其他分支修改代码后，仓库主页面会出现按钮：`Compare & pull request`  
![img](https://github.com/dvbb/ProjectAndTestModule/blob/main/GitTutorial/image/step1.png)

2. 按下按钮后到达新界面:  
![img](https://github.com/dvbb/ProjectAndTestModule/blob/main/GitTutorial/image/step2.png)  
此时界面有Base 和 Compare 两个选项。Base 是你希望提交到的分支，Compare 是你工作的分支。

3. 填写说明  
![img](https://github.com/dvbb/ProjectAndTestModule/blob/main/GitTutorial/image/step3.png)  
> draft pr 能够生产PR页面、触发ci，同时不会添加Reviewer group。在发起正式PR之前可以先提draft查看ci是否通过、通过git compare审核代码。