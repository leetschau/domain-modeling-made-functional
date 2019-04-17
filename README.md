# Intro

This is my notes and exercises of Domain Modeling Made Functional:
Tackle Software Complexity with Domain-Driven Design and F#
by Scott Wlaschin.

Official source code: [main page](https://pragprog.com/book/swdddf/domain-modeling-made-functional).

# Loading Scripts

On Ubuntu, load files with:
```
$ fsharpi
> #load "Chapter5.fsx";;
```

On Windows, start REPL with `fsi` instead of `fsharpi`.

# 术语列表

* Bounded context: BC；

* Event: workflow 的输出，只在 BC 外部生成，BC 内部不存在 event，如果 BC 内部的多个 workflow 有依赖关系时，之间将它们顺序连在一起 (p54);

* Command：由某个 event 激活；

* Workflow: 一个工作流程，由一个 command 触发，输出一个或多个 event，实现形式为函数，输入是一个 command 对象，输出是一个 event 对象列表；

* Domain object: BC 内部的数据对象；

* Data transfer objects: DTO，包含在一个 event 中，在 BC 间传递信息的序列化对象，DTO 可以嵌套，例如 OrderPlaced event DTO 包含 订单 (Order) DTO，订单 DTO 又包含商品列 (OrderLine) DTO；

* Trust boundary: 即 BC 的边界；

* input gate: trust boundary 上负责接收信息的关口，外部是 untrusted outside world，从 outside 进入 BC 时，input gate 负责对信息有效性（例如订单格式是否正确）进行校验，BC 内部则不需要考虑有效性问题；

* output gate: trust boundary 上负责向下游 BC 发送信息的关口，负责去掉 DTO 中无需外界了解的私有信息，保证信息安全和降低耦合度；

* Anti-Corruption Layer: ACL, 不同 BC 间，或者 BC 与外部世界间的词汇转换器，一般由 input gate 承担此职责；

* Context map: p21, p52（zathura 页码减10），包含各个 BC，BC 间的信息流向以及协议类型 (shared kernel/consumer driven/conformist)；

* Value Object: a domain object without identity. Two Value Objects containing the same data are considered identical. Value Objects must be immutable – if any part changes, it is a different Value Object. Examples of Value Objects are: Name, Address, Location, Email, Money, Date, etc;

* Entity: a domain object that has an intrinsic identity that persists even as its properties change. Entity objects generally have an Id or Key field, and two Entities with the same Id/Key are considered to be the same object. Entities typically represent domain objects that have a lifespan, with a history of changes, such as a document. Examples of Entities are: Customer, Order, Product, Invoice, etc;

* Aggregate: a collection of related objects that are treated as an atomic unit for the purpose of consistency and data transactions. Other entities should only reference the Aggregate by its identifier, which is the id of the “top-level” member of the Aggregate, known as the “root”;

