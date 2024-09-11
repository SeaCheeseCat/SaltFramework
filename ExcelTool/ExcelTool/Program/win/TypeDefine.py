# encoding: utf-8
############################################
#定义自定义公有类型，此处定义的类型将可以
#被用于excel表中使用
############################################
TypeList =[
    {
        "Name":"ItemType",
        "Fields":
        [
            {"name":"ID","type":"int","desc":"道具ID"},
            {"name":"Type","type":"int","desc":"物品类型","count":3},
            {"name":"Name","type":"string","desc":"道具名称"},
        ],
        "Desc":"test item type"
    },
]
