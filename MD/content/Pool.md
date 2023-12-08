**对象池**

这里是对象池！！可以有效节约资源使用，写了两种对象池x

一种比较常用的预制体对象池，一种是类对象池



- 预制体对象池
- 类对象池





**[预制体对象池]**

如何创建一个对象池?



打开PoolManager类 

增加一个新的对象  



`public GameObjectPool TextChatUnitPool;   //聊天对象池`



在Init方法中

实例化它

 `TextChatUnitPool = new GameObjectPool(1, "TextChatUnitPool", poolRoot.transform, 5);`

参数解析：   

   	1.对象池ID（唯一ID） 
   	2.对象池名称（支持创建多个对象池）
   	3. 对象池挂载位置
   	4. 对象池允许的最大容量



