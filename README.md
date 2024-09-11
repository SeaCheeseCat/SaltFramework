# SaltFramework框架
一个自由极简的框架，可以帮助你快速的完成游戏原型的制作，提升开发效率
为你的独立之路助力！

[![license](http://img.shields.io/badge/license-MIT-green.svg)](https://opensource.org/licenses/MIT) 
[![Unity Version](https://img.shields.io/badge/unity-2021.3.15f1-blue)](https://unity.com) 
[![Platform](https://img.shields.io/badge/platform-Win%20%7C%20Android%20%7C%20iOS%20%7C%20Mac%20%7C%20Linux%20%7C%20WebGL-orange)]() 

> 使用它你不需要额外引入任何命名空间，做到拿到即用！

## 架构简述

*SaltFramework对常用的几大模块进行了统一的封装，包括：资源加载、消息传递、对象池... ...*

*在当前的框架版本中包含了17种内置模块，目前我也在进行游戏开发，会将遇到的一些新的需求持续更新到框架中去*



**它包含了:**

###                                                          ***▷ 核心部分***

- **资源管理** - ResourceManager

  ​	 *负责管理游戏内的各类资源加载。它封装了异步和同步两种资源加载方式，提供更灵活的加载方式*

  

- **消息传递机制** - MsgManager

  ​	*用于实现松耦合消息传递和广播的核心模块。它允许对象之间	通过消息系统进行通信，无需相互直接引用，适用于事件驱动的场景。它允许对象进行发送，注册事件*

  

- 游戏音频管理器 - AudioManager     

  ​	*游戏内管理和控制音频播放的核心模块，负责管理背景音乐 、音效 、语音等各类音频的播放、暂停、停止，渐入渐出等操作*



- 对象池 - PoolManager
  
  ​	 *用于管理游戏对象池的模块，通过对象池优化游戏中频繁创建和销毁对象的操作，减少性能开销。*
  
  
  
- FSM有限状态机 - AIStateBase

​			*为每个单位提供了清晰的行为逻辑管理，通过在不同的状态之间进行切换，来控制单位的行为。有限状态机简单、高效，适合用来实现各种 AI 行为。*




- Excel配置表 - Config

  ​      *从 Excel 配置表中读取和管理游戏数据。通过将数据存储在 Excel 表中，可以方便地进行数据修改和管理，减少代码中的硬编码。*

  ​	

- 多语言模块 - LanguageManager

  ​    *管理游戏中多语言支持，通过加载和切换不同语言的文本资源，使游戏能够支持多个语言版本。*
  
  


- 存档模块 - ArchiveManager 

  ​    *用于管理游戏存档，提供数据的加密、解密和序列化功能。通过Json进行储存*
  
  


- UI模块 - UIManager、UIBase

  ​      *用于处理游戏中的所有UI相关操作，封装了统一的UI注册，打开，关闭接口，在任意窗口都能快捷打开的UI*

  

- 单例模式 - Singleton

​			*单例模式确保一个类在整个应用程序中只有一个实例，并提供全局访问该实例的方法。*




- 数据管理 - GameBaseData

  ​		*使用静态字段来存储和访问全局数据，方便在整个应用程序中访问和更新这些数据*

  

- 事件管理模块 - EventManager 

  ​		*允许通过字符串和反射来调用事件。使得事件系统更加灵活，并且可以方便地与 Excel 表格中的数据配合使用，支持动态事件触发。*

  

- 按键管理模块 - InputKeyManager 

  ​		*提供了统一的管理按键的按下、释放和触发检测功能，并实现了按键状态的缓存机制，并且包含快速接入长按*

  ​	  


- Log模块 - DebugEx 

  ​		*用于提供更灵活和符合直觉的日志记录方式。它扩展了 Unity 的 Debug 类，支持更清晰和结构化的日志输出，同时包含帧率检测*

  
  
- 场景加载模块 - LoadManager 

  ​		*管理场景加载的模块。它提供了场景的异步加载、卸载以及过渡效果的支持*
  
  
  
- 动画模块 - AnimationManager 

  ​		*供了灵活的接口来快速实现物体运动、旋转、缩放等动画效果（非补间动画）需要继承AnimationBase进行使用*
  
  
  
- 地图编辑器 - MapConfig 
  
  ​		*用于自动记录和生成场景的模块，支持动态加载游戏关卡。与传统的一个场景一个文件的方式不同，`MapConfig` 允许通过配置文件和程序逻辑动态生成和加载场景，以提高游戏开发的灵活性和效率。*
  
  
  
  ###                                                  ***▷ 编辑器拓展部分***
  
- 框架核心配置 - FrameworkEditor

  - 项目设置
  - 多语言配置
  - Document文档

- 资源路径快速打开 - PathEditor

  - 快速打开存档路径、清空存档
  - 打开配置表路径、生成配置

- 表格解析器 - ExcelEdiotr
  
     - 直观的Excel管理工具，支持生成F4一键生成配置
     - 创建Excel模板


- 地图编辑器 - LevelEditor
    - 动态生成关卡配置
    - 读取关卡
  
- 重启项目 - EditTool 


- 快速场景切换 - ScenesMenuBuild

- 帧率显示 - FPSGUI
  
    
  
    


  **配备插件**

  - 快速Shared切换 [AllIn1SpriteShader]
  - DoTween动画插件
  - ProtoBuf 一种高效的数据传输模式
  - LitJson Json解析插件

## 快速开始

1. 通过下方安装目录 将package包直接导入到你的项目中，导入完成后即可看到框架的拓展部分

2. 创建正确的Salt框架结构

    可以打开Scenes —> Temple场景 那里有我为你准备的最基础的场景结构

    一个正确的场景结构应该包括： UIBase（用于UI的创建锁定）、GameManager、GameBase（游戏启动器）、MapConfig（可选的地图编辑器）

3. 点击运行查看框架是否正确载入AudioManager、PoolManager等模块



### 运行环境

* Unity 2018.4.x ~ 2023.x

## 安装

- FrameworkCode

  - ​	Clone下本项目将Framework文件夹放到你的游戏中去
  
- Package安装
  - ​	[点此下载 unitypackage](Package/CheeseFramework.unitypackage)
    


 ![emoji1](Document/Item/emoji1.jpg)

 阴暗地爬行.jpg



## **社媒：**

*总之是一款 近些年写游戏的总结，慢慢写出了一套框架出来。希望对你有所帮助，我应该会持续更新这个库，目前也在锐意开发新作中，有新的构想会慢慢加上来*



[B站](https://space.bilibili.com/442876378)、[微博](https://weibo.com/u/7242984074)   （有时间也许会更新视频教程（也许不会））



