# Uixe.Watcher 改造实施说明（正式版）

## 一、项目定位

`Uixe.Watcher` 当前不是普通的 Web 项目，而是以下混合结构：

- `WinForms` 主程序
- 进程内自托管 `ASP.NET Core`
- 外部系统调用本机 `9999`
- `Controller` 直接操作 `frmPlaza`
- API 收到请求后直接驱动桌面窗体

当前核心链路为：

`外部系统 -> 本机内嵌 API -> WinForms 窗体`

这意味着本项目不能一次性硬重写，必须遵循：

- 先兼容，后替换
- 先解耦，后迁移
- 先独立后端，再逐步切换 UI

---

## 二、目标架构

### 1. 目标形态

标准目标链路：

`外部系统 -> 中心后端 API -> Application Service -> 数据存储/缓存 -> SignalR -> Web 前端`

如需保留本地能力，则补充兼容链路：

`中心后端 -> Agent -> 本地弹窗/语音/VNC/设备能力`

### 2. 最终推荐技术栈

#### 后端

- `ASP.NET Core 10 Web API`
- `SignalR`
- `Quartz`
- `Swagger / OpenAPI`
- `MemoryCache` 或 `Redis`
- `PostgreSQL` 或 `SQL Server`

#### 前端

- `Vue 3`
- `TypeScript`
- `Vite`
- `Pinia`
- `Vue Router`
- `Arco Design Vue`
- `Tailwind CSS`
- `ECharts`
- `SignalR JavaScript Client`

### 3. 当前已落地的新项目

当前仓库已新增并落地以下项目：

#### 后端

- `src/Uixe.Copilot.Api`
- `src/Uixe.Copilot.Application`
- `src/Uixe.Copilot.Contracts`
- `src/Uixe.Copilot.Domain`
- `src/Uixe.Copilot.Infrastructure`

#### 前端

- `src/Uixe.Copilot.Web`

#### 测试

- `tests/Uixe.Copilot.Api.Tests`
- `tests/Uixe.Copilot.Application.Tests`

当前说明：

- `Uixe.Copilot.Api` 已提供健康检查和事件总览业务接口
- `Uixe.Copilot.Web` 已提供深色监控首页、事件中心、详情页，并已开始对接真实后端接口
- 旧 `WinForms` 与新 `Web` 仍保持并行迁移模式

#### 可选兼容层

- `Uixe.Watcher.Agent`
- 本地通知
- 本地语音播报
- 本地 `VNC` 调起
- 旧接口兼容转发
- `WebView` 承载新 Web 前端

---

## 三、为什么前端推荐 `Arco Design Vue + Tailwind CSS`

不继续推荐 `Element Plus`，原因如下：

- 深色主题表现更好
- 企业组件完整，足够支撑监控类系统
- 更适合做监控中心、告警中心、大屏卡片、事件弹窗
- 配合 `Tailwind CSS`，更容易做出品牌化和科技感界面

### 建议的前端视觉风格

- 深色科技风
- 玻璃拟态卡片
- 渐变高亮
- 轻动效
- 大号数字指标卡
- 站点态势图 + 事件时间流 + 媒体预览

可选点缀：

- `Motion One` 或 `GSAP`
- `Lottie`
- 局部 `Three.js` 背景粒子效果

说明：以上增强只做点缀，不作为核心依赖。

---

## 四、核心改造原则

### 1. 先兼容旧接口，不先强制改上游,不直接删除现有内容。 现有界面和新web界面可以并存，可以切换。 

优先兼容当前请求契约，减少联调成本。现阶段重点兼容以下入口：

- `LaneController`
- `TrafficEvent([FromBody] TrafficEventPushRequest)`
- `Bulktrans(string, BulklyDto)`
- `bill_info(string, BillInfoDto)`
- `ConfirmEnInfo`

### 2. `Controller` 不再直接操作 UI

必须逐步改成：

- `Controller` 只负责接收请求
- 做参数校验与请求映射
- 调用 `Application Service`
- 输出标准结果模型
- 写库 / 推送 `SignalR`
- 展示由 Web 或 Agent 决定

### 3. UI 展示与业务处理彻底分离

以下能力必须抽离出窗体层：

- 车道匹配
- 站点定位
- 交通事件入队
- 媒体数据整理
- 告警去重
- 播报策略
- 推送通知

### 4. `WinForms` 降级为展示端或 Agent 能力层

后续 `WinForms` 不再承担核心业务编排，只保留：

- 本地展示
- 本地操作入口
- 兼容适配
- 本地设备能力

---

## 五、正式开工实施清单 V1

以下内容可直接作为发给 `GitHub Copilot` 的开工说明。

### 开工目标

请基于当前 `Uixe.Watcher` 代码库，按“先兼容、后替换、逐步解耦”的原则，启动第一版架构改造。

### 第一阶段必须完成的事情

#### 1. 梳理当前系统边界

请先分析并输出以下内容：

- 当前 API 接口清单
- 当前 DTO 清单
- 直接依赖 `frmPlaza` / `frmMain` 的代码点
- 当前事件处理主链路
- 必须保留的本地能力清单
- 上游系统依赖点清单

重点扫描文件：

- `Program.cs`
- `Startup.cs`
- `Controllers/LaneController.cs`
- `Services/TrafficEventQueueService.cs`
- `WinForms/frmMain.cs`
- `WinForms/frmPlaza.cs`

#### 2. 建立新的分层结构

请按以下职责准备项目边界：

- `Uixe.Watcher.Api`
  - 对外 HTTP API
  - Swagger/OpenAPI
  - SignalR Hub
- `Uixe.Watcher.Application`
  - 应用服务
  - 用例编排
  - 业务流程组织
- `Uixe.Watcher.Domain`
  - 实体
  - 枚举
  - 领域规则
  - 值对象
- `Uixe.Watcher.Infrastructure`
  - 数据访问
  - 缓存
  - 外部接口
  - Quartz 任务
  - 推送与集成
- `Uixe.Watcher.Contracts`
  - DTO
  - 请求响应模型
  - 共享枚举
  - SignalR 消息契约

#### 3. 拆掉 `Controller -> Form` 直接调用

重点要求：

- `LaneController` 不再直接访问 `frmPlaza`
- `Controller` 只负责接收请求和调用应用服务
- 把原有窗体驱动逻辑迁移到应用服务接口后面
- 输出标准化结果对象

#### 4. 抽离事件处理主链路

围绕 `TrafficEventQueueService` 拆出核心服务：

- `EventIngestionService`
- `LaneMatchingService`
- `MediaResolverService`
- `RealtimePushService`
- `NotificationService`
- `SystemConfigService`

#### 5. 保留旧接口兼容层

现阶段要求：

- 外部上游调用方式尽量不变
- 旧接口先兼容
- 内部逐步映射到新的应用服务
- 短期允许适配层存在

---

## 六、当前实施进度（2026-03-21）

### 1. 已完成

#### 架构与工程骨架

- 已新增并接入新的后端分层项目：
  - `src/Uixe.Copilot.Api`
  - `src/Uixe.Copilot.Application`
  - `src/Uixe.Copilot.Contracts`
  - `src/Uixe.Copilot.Domain`
  - `src/Uixe.Copilot.Infrastructure`
- 已新增测试项目：
  - `tests/Uixe.Copilot.Api.Tests`
  - `tests/Uixe.Copilot.Application.Tests`
- 已形成 `Uixe.Copilot` 命名体系，并纳入现有解决方案。

#### 第一阶段解耦

- `LaneController` 已改为通过 `ILaneApplicationService` 调用，不再直接承担主要业务编排。
- `TrafficEventQueueService` 已引入展示处理抽象，避免继续直接绑定具体窗体实现。
- 已建立兼容适配层与映射扩展，覆盖部分旧 DTO 到新契约的转换。
- 已把部分窗体/宿主上下文整理为应用层可消费的上下文服务，如 `IPlazaContextService`。

#### TrafficEvent 主链路第一版

- 已新增 `TrafficEventsController`：
  - `GET /api/traffic-events/overview`
  - `GET /api/traffic-events/{eventId}`
  - `POST /api/traffic-events`
- 已新增 `TrafficEventApplicationService`，具备基础校验、车道匹配、实时推送触发能力。
- 已新增 `TrafficEventQueryService`，当前已基于内存仓储返回真实提交后的总览与详情数据。
- 已新增 `TrafficEvent` 领域实体，并将内存事件仓储下沉到 `Uixe.Copilot.Infrastructure`。

#### Web 前端一期

- `src/Uixe.Copilot.Web` 已存在并可作为新 UI 一期工程。
- 已接入监控总览、事件中心、事件详情、收费站监控、历史查询、系统配置等页面骨架。
- 前端已开始对接真实后端接口，而不只是静态 mock。
- 历史查询页面已接入后端 `history` 接口，可按收费站、事件类型、状态进行基础过滤。
- 历史查询页面已支持基础分页与时间范围过滤。
- 系统配置页面已接入后端 `system-settings` 接口，可读取并保存基础播报/通知/主题配置。
- 事件详情页已接入基础媒体预览，可展示事件图片与视频地址。

#### 实时通信第一版

- 已新增 `TrafficEventsHub`。
- 已新增 `SignalRTrafficEventPushService`。
- 前端已建立 SignalR 连接，并在交通事件提交后触发刷新。

#### 工程治理

- 已补充 `.gitignore`，忽略 `bin`、`obj`、`TestResults` 等产物。
- 新增后端测试项目已可运行，最近一次 `Uixe.Copilot.Application.Tests` 测试通过。

### 2. 进行中

- `LaneApplicationService` 仍是兼容编排入口，内部还保留部分 WinForms/宿主相关协作逻辑，尚未完全收敛为纯兼容外壳。
- `TrafficEvent` 主链路已通，查询已支持内存仓储、文件持久化仓储与 SQLite 数据库仓储三种模式，后续再继续演进到 PostgreSQL / SQL Server。
- `TrafficEvent` 主链路已完成 API 层文件持久化模式联通验证，可在不依赖外部数据库服务时完成基本落盘与回查。
- Web 前端已完成历史查询、系统配置、媒体预览的基础联通，但仍缺少导出、完整配置域、专用媒体组件与多媒体增强能力。

### 3. 尚未完成

#### 第二阶段：实时与存储

- 事件持久化能力未完成。
- 事件持久化已具备文件模式与 SQLite 模式最小实现，但 PostgreSQL / SQL Server 正式实现仍未完成。
- 站点/车道状态缓存体系未完成。
- 历史查询接口已打通，但读模型仍为基础版，尚未支持分页、导出、复杂筛选。
- 历史查询接口已支持基础分页与时间范围，但导出与更复杂筛选仍未完成。
- 历史查询接口已修正分页总数语义，当前 `Total` 为符合筛选条件的总记录数。
- 更丰富的 SignalR 消息契约与订阅粒度未完成。

#### 第三阶段后半段：Web 前端深化

- 历史查询页面已对接真实后端，并支持基础筛选、分页和时间范围。
- 系统配置页面已接入基础真实配置模型，但尚未扩展到完整系统配置域。
- 媒体预览链路已完成基础版，但尚未按最终要求落到更完整的专用媒体组件与多媒体列表能力。

#### 第四阶段：Agent 兼容层

- `Uixe.Watcher.Agent` 尚未创建。
- 本地通知、语音、VNC、WebView 承载等能力尚未迁移到独立 Agent 形态。

#### 联调与灰度

- 尚未完成上游联调回归清单。
- 尚未完成灰度切换方案与回滚预案验证。

### 4. 当前建议的直接下一步

按迁移计划，下一步优先进入“第二阶段：实时与存储”的第一批实现：

1. 为 `TrafficEvent` 建立应用层事件仓储抽象。
2. 将提交成功的事件保存到内存仓储中，替代当前纯示例查询数据。
3. 让 `overview` / `detail` 基于真实提交结果返回。
4. 为后续接入数据库保留基础边界。

这一步完成后，新后端链路会从“演示型接口”进入“可积累、可查询、可回放”的真实业务形态。

当前这一步已完成，下一步建议继续：

1. 在 `Infrastructure` 中补充数据库版仓储实现。
2. 将 `History` 查询页面接到统一查询服务。
3. 补充分页、筛选、状态流转等读模型能力。

### 第一批优先改造文件

按优先级执行：

1. `Program.cs`
   - 去掉“WinForms 进程内自托管 API”作为长期核心模式
   - 为独立后端启动方式做准备
2. `Startup.cs`
   - 保留现有服务注册经验
   - 后续迁移到独立后端项目
3. `Controllers/LaneController.cs`
   - 去掉对 `frmPlaza` 的直接访问
   - 改为调用应用服务接口
4. `Services/TrafficEventQueueService.cs`
   - 去掉对窗体的直接依赖
   - 改成标准事件处理与推送流程
5. `WinForms/frmMain.cs`、`WinForms/frmPlaza.cs`
   - 从业务承载层降级为展示端或 Agent 本地能力层

### 输出要求

请按以下顺序输出分析与实施建议：

1. 当前代码解耦分析结果
2. 建议新增的项目与目录结构
3. 建议新增的核心接口与类清单
4. 第一批应迁移的方法列表
5. `LaneController` 改造方案
6. `TrafficEventQueueService` 改造方案
7. 风险点与兼容策略
8. 可分批提交的实施步骤

### 实施约束

- 不要一次性重写全部系统
- 不要先删除 `WinForms`
- 不要先要求上游系统改接口
- 先建立独立业务层，再逐步迁移
- 所有新代码优先面向未来独立 `Web API`
- 可以短期保留兼容适配层

---

## 六、推荐目录结构

建议将仓库整理为“后端 + 前端 + Agent + 测试 + 文档”的结构。

```text
D:\Uixe\uixe-watcher\
├─ src\
│  ├─ Uixe.Watcher.Api\
│  │  ├─ Controllers\
│  │  ├─ Hubs\
│  │  ├─ Middleware\
│  │  ├─ Extensions\
│  │  ├─ Program.cs
│  │  └─ appsettings.json
│  │
│  ├─ Uixe.Watcher.Application\
│  │  ├─ Abstractions\
│  │  ├─ Services\
│  │  ├─ Features\
│  │  │  ├─ TrafficEvents\
│  │  │  ├─ Lanes\
│  │  │  ├─ Plaza\
│  │  │  └─ Notifications\
│  │  ├─ Commands\
│  │  ├─ Queries\
│  │  └─ DependencyInjection.cs
│  │
│  ├─ Uixe.Watcher.Domain\
│  │  ├─ Entities\
│  │  ├─ ValueObjects\
│  │  ├─ Enums\
│  │  ├─ Events\
│  │  ├─ Rules\
│  │  └─ Constants\
│  │
│  ├─ Uixe.Watcher.Infrastructure\
│  │  ├─ Persistence\
│  │  │  ├─ Configurations\
│  │  │  ├─ Repositories\
│  │  │  └─ Migrations\
│  │  ├─ Caching\
│  │  ├─ Integrations\
│  │  ├─ Jobs\
│  │  ├─ Messaging\
│  │  └─ DependencyInjection.cs

---

## 七、当前已实现的 Web UI 一期

当前 Web UI 已按一期范围启动，项目位置：

- `src/Uixe.Copilot.Web`

已完成页面：

- 监控总览
- 收费站监控
- 事件中心
- 事件详情
- 历史查询（骨架）
- 系统配置（骨架）

已完成内容：

- 深色科技风布局
- Arco Design Vue + Tailwind CSS 样式骨架
- Pinia 状态管理
- Vue Router 路由
- 对接 `Uixe.Copilot.Api` 的事件总览与详情接口

当前后端接口：

- `GET /api/health`
- `GET /api/traffic-events/overview`
- `GET /api/traffic-events/{eventId}`
- `POST /api/traffic-events`
- `SignalR /hubs/traffic-events`

当前实时推送状态：

- `Uixe.Copilot.Api` 已新增 `TrafficEventsHub`
- `TrafficEvent` 提交成功后会触发实时广播
- `Uixe.Copilot.Web` 已接入 SignalR 客户端骨架，并在收到 `trafficEventSubmitted` 后刷新总览

前端启动方式：

1. 启动后端：

`dotnet run --project src/Uixe.Copilot.Api/Uixe.Copilot.Api.csproj`

2. 启动前端：

进入 `src/Uixe.Copilot.Web` 目录后执行：

`npm install`

`npm run dev`

如需修改 API 地址，可配置：

- `VITE_API_BASE_URL`
│  │
│  ├─ Uixe.Watcher.Contracts\
│  │  ├─ Requests\
│  │  ├─ Responses\
│  │  ├─ Dtos\
│  │  ├─ Enums\
│  │  └─ SignalR\
│  │
│  ├─ Uixe.Watcher.Agent\
│  │  ├─ CompatibilityApi\
│  │  ├─ LocalNotifications\
│  │  ├─ Voice\
│  │  ├─ Vnc\
│  │  ├─ Tray\
│  │  ├─ WebView\
│  │  └─ Program.cs
│  │
│  └─ Uixe.Watcher.WinForms.Legacy\
│     ├─ WinForms\
│     ├─ Controls\
│     ├─ TCO\
│     ├─ Services\
│     ├─ Controllers\
│     └─ Program.cs
│
├─ web\
│  └─ uixe-watcher-web\
│     ├─ src\
│     │  ├─ api\
│     │  ├─ components\
│     │  ├─ layouts\
│     │  ├─ pages\
│     │  ├─ router\
│     │  ├─ stores\
│     │  ├─ signalr\
│     │  ├─ styles\
│     │  └─ utils\
│     ├─ public\
│     └─ package.json
│
├─ tests\
│  ├─ Uixe.Watcher.Api.Tests\
│  ├─ Uixe.Watcher.Application.Tests\
│  ├─ Uixe.Watcher.Domain.Tests\
│  ├─ Uixe.Watcher.Infrastructure.Tests\
│  └─ Uixe.Watcher.IntegrationTests\
│
├─ docs\
│  ├─ architecture\
│  ├─ api\
│  ├─ migration\
│  └─ traffic-event\
│
└─ Uixe.Watcher.sln
```

---

## 七、项目命名建议

### 1. 后端项目

建议统一使用以下命名：

- `Uixe.Watcher.Api`
- `Uixe.Watcher.Application`
- `Uixe.Watcher.Domain`
- `Uixe.Watcher.Infrastructure`
- `Uixe.Watcher.Contracts`

### 2. 兼容层 / 桌面代理

建议命名：

- `Uixe.Watcher.Agent`

用于承担：

- 本地接口兼容
- 消息转发
- 语音提醒
- 本地通知
- `VNC`
- `WebView` 承载

### 3. 旧桌面项目

建议将现有桌面项目逐步调整定位为：

- `Uixe.Watcher.WinForms.Legacy`

这样命名最清楚，表示它属于过渡期遗留壳层，而不是未来核心。

### 4. 前端项目

前端建议单独保留前端生态命名：

- `uixe-watcher-web`

如果后续拆分管理后台或大屏，可继续扩展：

- `uixe-watcher-admin`
- `uixe-watcher-screen`

### 5. 测试项目

建议采用以下命名：

- `Uixe.Watcher.Api.Tests`
- `Uixe.Watcher.Application.Tests`
- `Uixe.Watcher.Domain.Tests`
- `Uixe.Watcher.Infrastructure.Tests`
- `Uixe.Watcher.IntegrationTests`

---

## 八、核心服务命名建议

### 服务类

建议统一使用 `Service` 后缀：

- `EventIngestionService`
- `LaneMatchingService`
- `RealtimePushService`
- `NotificationService`
- `MediaResolverService`
- `SystemConfigService`

### 接口

建议统一使用 `I` 前缀：

- `IEventIngestionService`
- `ILaneMatchingService`
- `IRealtimePushService`
- `INotificationService`
- `IMediaResolverService`
- `ISystemConfigService`

### 控制器

兼容旧接口阶段可保留现有语义：

- `LaneController`
- `TrafficEventController`
- `CompatibilityController`

未来新接口建议采用标准资源命名：

- `EventsController`
- `LanesController`
- `PlazasController`
- `NotificationsController`

---

## 九、后端模块建议

后端建议至少具备以下核心模块：

- `Gateway / Controller`
- `EventIngestionService`
- `LaneMatchingService`
- `NotificationService`
- `RealtimePushService`
- `MediaResolverService`
- `SystemConfigService`

### 数据层建议

不建议继续以 `LiteDB` 作为未来中心架构核心存储。

原因：

- 它更适合本地单机
- 不适合多用户集中访问
- 不适合复杂统计查询
- 不适合历史追溯和并发处理

建议演进到：

- `PostgreSQL`：性价比高，适合事件类数据
- `SQL Server`：如果现场更偏微软栈

---

## 十、前端一期页面规划

建议第一期先落地 6 个主页面：

### 1. 监控总览

- 全站点状态
- 在线率
- 告警数量
- 今日事件趋势

### 2. 收费站监控

- 站点列表
- 车道状态
- 实时刷新
- 颜色态势标识

### 3. 事件中心

- 交通事件流
- 特情告警
- 语音/通知状态
- 处理状态

### 4. 事件详情

- 基础信息
- 图片预览
- 视频预览
- 历史关联记录

### 5. 历史查询

- 时间筛选
- 站点筛选
- 事件类型筛选
- 导出

### 6. 系统配置

- 播报策略
- 用户权限
- 对接参数
- 主题设置

### 前端视觉规范

- 主色：深蓝 / 电紫 / 青蓝渐变
- 卡片：半透明毛玻璃
- 告警：橙 / 红高亮
- 状态：绿 / 黄 / 红
- 动效：轻量，不花哨过头
- 图表：高对比夜间风格

---

## 十一、Agent 是否保留

建议保留 `Agent`，但仅承担轻量本地能力，不再承担核心业务。

### 保留原因

浏览器不适合直接替代以下能力：

- 本地语音播报
- `Windows` 系统通知
- 本地 `VNC` 调起
- 局域网或设备访问能力
- 兼容上游仍推送本机 `9999`

### Agent 职责

- 监听旧接口
- 转发到中心后端
- 接收中心推送
- 执行本地提醒能力
- 承载 `WebView` 运行新前端

---

## 十二、分阶段实施计划

### 第 0 阶段：梳理与冻结接口

时间：`2~3` 天

输出：

- 现有接口清单
- DTO 清单
- 页面清单
- 保留能力清单
- 上游系统对接清单

### 第 1 阶段：后端解耦

时间：`1~2` 周

工作：

- 拆掉 `Controller -> Form` 直接调用
- 提炼 `Application Service`
- 提炼共享 DTO 与领域模型
- 独立出 `Uixe.Watcher.Api`

输出：

- 可独立启动的后端服务
- 与 `WinForms` 无关的业务层

### 第 2 阶段：实时通信与数据落库

时间：`1` 周

工作：

- 接入 `SignalR`
- 事件入库
- 站点 / 车道状态缓存
- 历史查询模型

输出：

- 后端可接请求
- 后端可存储
- 后端可推送

### 第 3 阶段：Web 前端一期

时间：`2~3` 周

工作：

- 登录与权限
- 总览页
- 收费站监控页
- 事件中心
- 详情页
- 媒体预览

输出：

- 可用的 Web 监控端

### 第 4 阶段：Agent 兼容层

时间：`1~2` 周

工作：

- 本地接口兼容
- 语音提醒
- 本地通知
- `VNC` 能力迁移

输出：

- 可逐步替换旧客户端

### 第 5 阶段：联调与灰度

时间：`1` 周

工作：

- 上游联调
- 多站点试运行
- 压测
- 回滚预案

输出：

- 灰度上线版本

---

## 十三、上线策略

### 方案 A：最稳妥

- 先上线后端
- `WinForms` 继续运行
- 新 Web 前端并行上线
- 当前客户端窗口内可通过 `WebView` 承载新页面
- 一段时间后逐步停用旧 UI

### 方案 B：带 Agent 的平滑过渡

- 旧客户端逐步改造成 `Agent`
- 上游先不改推送地址
- `Agent` 转发到中心后端
- 前端统一走 Web

结论：当前项目更适合 **方案 B**。

---

## 十四、最终统一建议

最终建议的统一形态为：

- 后端：`ASP.NET Core 10 Web API + SignalR + Quartz`
- 前端：`Vue 3 + TypeScript + Vite + Arco Design Vue + Tailwind CSS + ECharts`
- 可选兼容层：`Windows Agent`

### 选择原因

- 能真正完成前后端分离
- 保留实时监控能力
- 保留本地能力扩展口
- 界面风格更适合监控与告警场景
- 落地风险可控，适合渐进式迁移

---

## 十五、当前实施进展补充

### 1. 新后端工程

当前仓库已新增 `Uixe.Copilot` 分层后端骨架：

- `src/Uixe.Copilot.Api`
- `src/Uixe.Copilot.Application`
- `src/Uixe.Copilot.Contracts`
- `src/Uixe.Copilot.Domain`
- `src/Uixe.Copilot.Infrastructure`

### 2. 新 Web UI 工程

按照本文档前端规划，已新增新的 Web UI 骨架：

- `web/Uixe.Copilot.Web`

当前已落地页面骨架：

- 监控总览
- 收费站监控
- 事件中心
- 事件详情
- 历史查询
- 系统配置

当前 UI 采用：

- `Vue 3 + TypeScript + Vite`
- `Arco Design Vue`
- `Tailwind CSS`
- `ECharts`

并采用深色监控风格布局，后续逐步接入 `Uixe.Copilot.Api` 与 `SignalR`。
