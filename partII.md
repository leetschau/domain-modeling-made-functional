# Notes of Part II: Modeling the Domain

p59: 根据本部分内容生成的代码可做两种用途：

* 业务模型文档；

* 可编译的框架代码；

## Chapter 4

本章主要介绍 F# FP 语法，*Types and Functions* 一节中，对 *value* 和 *type* 含义的解释出现了循环定义：
一个 *value* 是 某个 *type* 的成员；
一个 *type* 是一组 *value* 的集合。

## Chapter 5

本章主要结合订单生成流程介绍了3个核心概念：value object, entity 和 aggregate，如何用 F# 类型代码表示，如何更新这些对象。

*More domain-driven design vocabulary* 这一节对上述概念做了很好的总结，最后在本章结束前再次强调了 代码即文本 的重要性。

## Chapter 6

使用 F# 的 *smart constructor* 技术实现对类型值的精确描述，例如：组件数量是一个 0 到 1000 之间的整数。
本章给出了使用 private constructor 的方法例子，但还有其他方法实现隐藏的构造器。

F# 的 `namespace` 在封装级别上相当于 Python 的 module，但其中只能包含类型 (type) 定义，type 中再包含 value 和 function 定义，它们不能直接定义在 namespace 中。

F# 的 `module` 相当于 Java 的 *static class*，其中可以包含 type 定义以及用 `let` 定义的东西，例如值和函数。`module` 可以被 *打开* (`open Module1`)，然后就可以直接使用 module 中定义的类型、函数或者值了，类似于 Python 的 `from module1 import *` 或者 Haskell 的 `import module1`。

所以 F# 的 类型 (type) 可以定义在 namespace 或者 module 中。
参考[What the difference between a namespace and a module in F#?](https://stackoverflow.com/questions/795172/what-the-difference-between-a-namespace-and-a-module-in-f)。

`WidgetCode` 以 `W` 开头，后跟4个数字的约束方法是：
```
module WidgetCode =

    /// Return the string value inside a WidgetCode
    let value (WidgetCode code) = code

    /// Create an WidgetCode from a string
    /// Return Error if input is null. empty, or not matching pattern
    let create fieldName code =
        // The codes for Widgets start with a "W" and then four digits
        let pattern = "W\d{4}"
        ConstrainedType.createLike fieldName WidgetCode pattern code
```

引用自 [Common.SimpleTypes.fs](https://github.com/swlaschin/DomainModelingMadeFunctional/blob/master/src/OrderTaking/Common.SimpleTypes.fs#L200).
奇怪的是这个实现在书中没有涉及。

