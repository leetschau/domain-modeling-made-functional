# Notes of Part I: Understanding the Domain

## Chapter 2

p48, Representing Constraints:
如何用 ADT 表示 *W* 开头，后跟4个数字 这样的数据类型？

## Chapter 3

C4 模型，从上层到下层包括：

1. System Context

1. Container: 一个可部署的东西，例如 Web server, Database, website 等；

1. Component: Container 中（按照业务逻辑划分的）最高层组件；一般对应于 DDD 中的 bounded contexts，各个 components 间应尽量降低耦合度，每个 component 高度自治(autonomous，尽量不依赖其他 components)；

1. Class: Component 中（按照实现逻辑划分的）最高层组件。

好的架构在 Container, Component 和 Class 之间划分合理的边界，以最小的成本应对需求变化。

系统建模一开始从单体应用开始，不需要考虑微服务，需要改微服务的时候（例如需要根据负载弹性伸缩，在内部组件级别上保证高可用等）再改不迟。
微服务的运维成本较高，当其中一个组件崩溃时，微服务应能够保证整个系统仍能正常运行（组件级别的高可用，类似于 Hadoop 中的多个 data node 互为备份），如果不能保证这一点，这个系统不叫微服务，而是一个分布式的单体系统。
这段分析十分精辟！

BC 间的消息传递可以使用消息队列 (Message Queue)，也可以直接采用函数调用 (function call)。

互相协作的 BC 间需要通过协议保证各自的格式定义一致，有3种方法：

* 商讨模式 (shared kernel)：处于协议范围内的规则，必须得到其他参与者同意才能修改；

* 下游驱动 (consumer driven)：由下游确定协议格式，上游负责按协议要求提供内容；

* 上游驱动 (conformist)：由上游确定协议格式，下游根据上游要求构建业务模型（似乎耦合度比较高）；

BC 内部不使用 event/handler 机制，太复杂。

### 代码结构

水平分层 (layer) 的结构设计是不好的，违背了 把需要一起变化的代码放在一起 原则，一个需求变更会导致各个层上的代码都要改动。DDD 的代码结构采用按 workflow 组织的方式（p56图），一个工作流改变只需修改它自身代码即可。

一个 workflow 内部采用 **Union architecture**，外层代码只依赖内层代码 (all dependencies must point inwards)。

问题：似乎应该是内层的业务代码依赖外层的数据库实现？还是这里对依赖的理解和我的意思相反？

FP风格要求我们尽量使用纯函数，将副作用集中起来，I/O只发生在业务层与数据层的边界上，不要在业务层内部执行I/O操作。

**Persistence Ignorance** 要求建模专注与业务本身，不要考虑任何与持久化有关的问题，例如如何设计表结构、表间关系如何等等，持久化方案的变化应该对业务模型没有任何影响。

