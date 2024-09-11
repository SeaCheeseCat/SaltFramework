
@echo 开始转表

@set unityPath=E:\Unity Project\Pugna_Battle\Assets
@set codePath=Assets/Script/Game/Managers/AdditionConfig/Types
@set dataPath=Assets/Resources/Config

@echo 正在清理数据
@del "out\code\*.*" /a /q
@del "out\data\*.*" /a /q

@echo 初始化静态数据
@if not exist "out\code" (
@md "out\code"
)
@if not exist "out\code\ResType" (
@md "out\code\ResType"
)
@if not exist "out\code\ResEnum" (
@md "out\code\ResEnum"
)
@if not exist "out\code\UserType" (
@md "out\code\UserType"
)
@if not exist "out\code\ResValue" (
@md "out\code\ResValue"
)
@if not exist "out\data" (
@md "out\data"
)

@echo 开始转换文件

@For /r xls %%i In (*.xlsx) Do @echo 正在转换  %%~ni & @python ExcelLoader.py %%i -all

@echo 开始复制文件

@del "%unityPath%\%codePath%\*.*" /a /q
@del "%unityPath%\%dataPath%\*.*" /a /q

#@xcopy "out\code" "%unityPath%\%codePath%" /S /E /Y
#@xcopy "out\data" "%unityPath%\%dataPath%" /S /E /Y

@del "out\code\*.*" /a /q
@del "out\data\*.*" /a /q

