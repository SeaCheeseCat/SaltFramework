# Git 入门指南

# 1. 序言：版本控制系统

简而言之，这是一种能够记录项目文件版本变化，以便查阅、比较甚至回退到特定历史版本的系统。

在多人协作项目中，使用版本控制系统能有效地处理每个人的工作，包括将它们合并到同一个云端仓库、检测到多人修改了同一个文件时提示冲突、在某人不小心毁掉某个文件时将它恢复到正常的版本等等。

# 2. Git 简介

Git 是目前世界上最先进的分布式版本控制系统，由Linux系统创始人开发，开源免费。

+ **基本使用方法**
  
  + 团队成员首先将云端仓库克隆（Clone）到本地；
  
  + 在本地做出任何需要的改动；
  
  + 经常将改动提交（Commit）到本地版本库；
  
  + 确认改动无误且可用后，将最终版修改结果推送（Push）到云端仓库；
  
  + 经常拉取（Pull）云端仓库最新的更新，以便和其他团队成员的进度同步。

+ **Git 协作模式的特点**
  
  + 每个人都可以克隆他人的仓库来创建自己的版本库，也可以将自己的版本库作为仓库提供给他人；
  
  + 每一次拉取都是对仓库的完整备份，单一版本库的丢失不会对项目产生影响；
  
  + 提交工作全部在本地版本库完成，无需连接网络且不会被他人打断；
  
  + 提供分支管理，允许团队成员创建多个分支同步推进进度，提高效率。

# 3. Git 下载与安装

首先下载 Git 客户端：[Git 官网下载地址](https://git-scm.com/download/win)

在 Standalone Installer 中根据电脑的操作系统进行选择（一般为64位）。

> Win10 可以在设置 → 系统 → 关于中查看系统类型

之后，运行下载好的安装程序。

+ Information
  
  使用许可声明，直接点击 Next 即可。

+ Select Destination Location
  
  选择安装路径，可以任意设置。如果弹出类似“The folder: xxx already exists.”的确认窗口，点击“是”。

+ Select Components
  
  选择组件，采用默认选项即可。

+ Select Start Menu Folder
  
  在开始菜单新建文件夹或选择已有文件夹，或直接左下角勾选不要文件夹（不推荐）。

+ Choosing the default editor used by Git
  
  选择 Git 默认编辑器，无需更改直接点击 Next 即可。

+ Adjusting the name of the initial branch in new repositories
  
  决定初始化新仓库的主分支名。此处推荐选择第二项 Override the default branch name of new repositories，并且保持该选项默认提供的 main 作为分支名。当然，如果保持默认选项使用 master 也没问题。（~~主要是因为在公司大家已经习惯了 main~~

+ Adjusting your PATH environment
  
  决定 PATH 环境变量，直接使用默认（Recommended）选项，点击 Next 即可。

+ Choosing the SSH executable
  
  选择 SSH 执行文件，使用默认选项（Use bundled OpenSSH）直接点击 Next 即可。

+ Chooing HTTPS transport backend
  
  选择 HTTPS 后端传输，使用默认选项（第一项）直接点击 Next 即可。

+ Configuring the line ending conversions
  
  设置行尾符号转换，使用默认选项（第一项）直接点击 Next 即可。

+ Configuring the terminal emulator to use with Git Bash
  
  设置 Git Bash 终端，使用默认选项（第一项）直接点击 Next 即可。

+ Choose the default behavior of "git pull"
  
  选择默认的拉取习惯，使用默认选项（第一项）直接点击 Next 即可。

+ Choose a credential helper
  
  选择凭证助手。若选择第一项，则在使用时（例如访问远程仓库时）可能需要用户名和密码验证；选择第二项（None）则不需要。此处可按需选择。

+ Configuring extra options
  
  设置额外选项，使用默认选项（仅选择第一项）直接点击 Next 即可。

+ Configuring experimental options
  
  设置实验选项，使用默认选项（全部不选）直接点击 Next 即可。

接下来，点击 Install 读条安装。安装成功后两个选项框都不用勾选，直接点击 Finish。

验证：在开始菜单找到刚刚安装的 Git，运行 Git Bash，输入 `git --version` 后按回车。如果返回版本信息，则说明安装成功。

参考文档：[Git 详细安装教程](https://blog.csdn.net/mukes/article/details/115693833)

# 4. 图形化操作 Git

默认状态下的 Git 只能使用命令行操作，~~除非是大佬或者只是想装逼不然~~由于其复杂繁琐所以不推荐这么做。更加方便的做法是安装图形化窗口，这也更符合 Windows 的操作习惯。

常见图形化软件为 TortoiseGit 和 SourceTree，此处仅介绍前者，后者~~因为没用过所以~~也可自行了解。

### 4.1 下载和安装

下载TortoiseGit：[TortoiseGit官网下载地址](https://tortoisegit.org/download/)

同样根据电脑的操作系统类型进行选择即可。如果需要，在该页下方还可下载汉化语言包。

以管理员身份运行msi安装程序，一直 Next 到 **Choose SSH Client**，选择第二项 **OpenSSH, Git default SSH Client**。之后继续 Next，安装路径也可以任意设置。最后安装完成时不需要勾选任何选项，直接点击 Finish 即可。

验证：在任意文件夹空白处点击右键，如果出现以下三个选项则说明安装成功。

```
- Git Clone...
- Git Create repository here...
- TortoiseGit
```

如果需要，此时可以安装汉化包msi，一直下一步直到安装完成即可。

### 4.2 配置选项

在任意文件夹空白处点击右键，选择 TortoiseGit → Settings，打开设置窗口。如果下载了汉化包，可以在此时将 General → Language 改为中文。同时，确认 **Git.exe Path** 是否为空，正常（已成功安装 Git）情况下这里应会自动定位到 `安装目录\Git\bin` 文件夹。

左侧选择 Git 一栏，在右侧的 User Info 中填入 Name 和 Email 信息。（由于使用 Github 作为仓库所以推荐填入 GitHub 的用户名和登录邮箱）

左侧选择 Network 一栏，确认 **SSH Client** 路径为 `安装目录\Git\usr\bin\ssh.exe` 。

最后，点击右下角的应用，再点击确定关闭窗口。

# 5. 使用 SSH 连接 Git

一般而言，使用 Git 连接 GitHub 进行协作时有 HTTPS 和 SSH 两种方式。由于 HTTPS 方式在国内连接极易超时失败，因此推荐使用 SSH 方式。

### 5.1 检查已有的 SSH Key

打开之前安装的 Git Bash，输入 `ls -al ~/.ssh`。如果看到类似以下输出，则说明这台电脑上已有 SSH Key。

```bash
$ ls -al ~/.ssh
total 18
drwxr-xr-x 1 UserName 197121 0 May 4 2023 ./
drwxr-xr-x 1 UserName 197121 0 Nov 10 15:05 ../
-rw-r--r-- 1 UserName 197121 411 May 4 2023 id_ed25519
-rw-r--r-- 1 Void 197121 102 May 4 2023 id_ed25519.pub
```

### 5.2 生成新的 SSH Key

如果没有密钥或不想使用旧密钥，也可重新生成新的 SSH Key。同样打开 Git Bash，输入：

```bash
$ ssh-keygen -t rsa -b 4096 -C "example@email.com"
```

其中邮箱最好替换为 Github 账号邮箱（不换也可以）。

控制台将返回一系列指令，要求输入若干参数。此处可以不用输入任何东西，直接连续按下回车。最终，控制台将返回一串 key 和它的字符图像（randomart image），这代表 SSH Key生成成功。

```bash
Generating public/private rsa key pair.
Enter file in which to save the key (/c/Users/UserName/.ssh/id_rsa):
Enter passphrase (empty for no passphrase):
Enter same passphrase again:
Your identification has been saved in /c/Users/UserName/.ssh/id_rsa
Your public key has been saved in /c/Users/UserName/.ssh/id_rsa.pub
The key fingerprint is:
SHA256:RrfSwnB6QYIF6TZFBevL2uDz5l4INZ4SuV8NYzWKAdw example@email.com
The key's randomart image is:
+---[RSA 4096]----+
|   .oO*oo o      |
|    +.E* o .     |
|   .o.* O .      |
|    +* X B .     |
|   .+.= S +      |
|     = * o       |
|    . = .        |
|   ..+..         |
|    o*=          |
+----[SHA256]-----+
```

### 5.3 添加 SSH Key 到账户

同样在 Git Bash 中，依次输入：

```bash
$ ssh-agent bash
$ ssh-add ~/.ssh/id_rsa #如果生成密钥时改过文件名和路径的话这里也要一起改
```

如果返回以下提示则说明添加成功。

```bash
Identity added: id_rsa (example@email.com)
```

### 5.4 添加 SSH Key 到 Github

在 Git Bash 中输入 `cat ~/.ssh/id_rsa.pub`（与上一步相同，如果生成密钥时修改过路径则此处一同修改），并复制返回的全部内容（以 ssh-rsa 开头，以邮箱结尾）。

打开GitHub，登录，点击右上角账户头像，在弹出的右边栏中选择 Settings。

在设置界面的左侧第二板块（Access 板块）找到 **SSH and GPG keys** ，点击，选择右上角的 **New SSH key** ，根据个人喜好填写 Title，再把刚刚复制的内容粘贴在下方的 key 输入栏里，最后点击 **Add SSH key** 。此时可能需要输入密码，按照提示输入后继续即可。

### 5.5 测试 SSH 连接

在 Git Bash 中输入 `ssh -T git@github.com` ，若弹出警告则输入 `yes` ，如果返回以下提示则说明连接成功。

```bash
Warning: Permanently added 'github.com,20.205.243.166' (ECDSA) to the list of known hosts.
Hi <your GitHub username> You've successfully authenticated, but GitHub does not provide shell access.
```

### 5.6 配置 Git 使用 SSH Key

获得所需仓库链接，然后选择一个文件夹点击右键 → Git Clone... ，在弹出的窗口中的URL 一栏填入链接（一般会自动从剪贴板获取），点击 OK，即可克隆仓库到本地。

进入仓库文件夹，右键打开 Git Bash，输入 `git remote -v` 。

若看到返回类似以下 git 开头的地址，则说明当前使用的已经是 SSH，无需修改；

```bash
origin git@example.git (fetch)
origin git@example.git (push)
```

若看到返回类似以下 https 开头的地址，则说明当前使用的依然是 https 协议。

```bash
origin https://example.git (fetch)
origin https://example.git (push)
```

此时，需要进入 GitHub 仓库，点击 Code → SSH，复制链接。

之后，在 Bash 窗口中输入 `git remote set-url origin git@github.com:example.git`

，其中 example 链接替换为刚刚复制的链接。

再次输入 `git remote -v` ，若返回 git 开头的地址，则说明设置成功。

至此，Git 协作环境搭建完毕，接下来就可以开始正式使用了！

# 6. 使用 Git 协作工作

本节将简单介绍 Git 基本操作。它们都将在工作中被高频使用，并且若无特殊说明，一切提到文件夹的情况都指克隆到本地的仓库文件夹。

### 6.1 提交 / Commit

在文件夹下点击右键 → Git Commit -> "main"... ，将会弹出提交窗口。提交步骤：

+ 在 Message 框中填写提交的内容（必填）；

+ 勾选 `Set author date` 和 `Set author` （勾选后右侧会显示时间和个人信息）；

+ 在下方文件列表中选择需要提交到仓库的文件（不涉及配置文件和代码的情况下一般是全选）；

+ 点击下方的提交 (Commit)，完成提交。

提交的目的是“将工作提交给版本管理工具 (Git) ”，此时工作内容依旧在本地，并未与其他工作组成员共享。

### 6.2 推送 / Push

在文件夹下点击右键 → Git Sync... ，将会弹出同步窗口。窗口中将显示之前提交的全部记录，此时点击推送 (Push) 即可将已提交内容推送到仓库。

推送之后，所有内容将传到云端，工作组成员均可访问。

### 6.3 拉取 / Pull

同样在文件夹下点击右键 → Git Sync... ，弹出的同步窗口中推送 (Push) 按钮的左侧就是拉取 (Pull) 按钮。点击此按钮即可将其他人推送到仓库的工作内容更新到本地。**任何时候开始工作前都推荐先进行拉取操作，以获取工作组成员最新的工作进展，保持本地仓库版本最新。**

### 6.4 冲突解决

在进行推送时，有时会显示红色的“有最新内容未拉取”的警告。此时需要先点击拉取，拉取成功后再重新尝试推送。Git 会自动合并已提交内容到最新版本，无需担心工作丢失。
