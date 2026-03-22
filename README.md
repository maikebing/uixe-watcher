# Uixe.Copilot 项目说明

## 一、项目定位

本仓库当前已经进入 **以 `Uixe.Copilot.Web + Uixe.Copilot.Api` 完整替代 `Uixe.Watcher`** 的实施阶段。

`Uixe.Watcher` 的历史形态是以下混合结构：

- `WinForms` 主程序
- 进程内自托管 `ASP.NET Core`
- 外部系统调用本机 `9999`
- `Controller` 直接操作 `frmPlaza`
- API 收到请求后直接驱动桌面窗体

当前核心链路为：

`外部系统 -> 本机内嵌 API -> WinForms 窗体`

当前策略已经调整为：

- 不再以旧 `WinForms` 宿主兼容运行为目标
- 旧 `Watcher` 仅作为迁移参考源码保留
- 展示功能统一迁入 `Uixe.Copilot.Web`
- 后端能力统一迁入 `Uixe.Copilot.Api`
- 本地桌面能力按需收敛到 `Uixe.Copilot.Agent`

---

## 二、目标架构

### 1. 目标形态

标准目标链路：

`外部系统 -> 中心后端 API -> Application Service -> 数据存储/缓存 -> SignalR -> Web 前端`

如需保留本地能力，则补充扩展链路：

`中心后端 -> Agent -> 本地弹窗/语音/VNC/设备能力`

### 2. 最终推荐技术栈

#### 后端

- `ASP.NET Core 10 Web API`
- `SignalR`
- `Quartz`
- `Swagger / OpenAPI`
- `MemoryCache` 或 `Redis`
- `PostgreSQL`

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

#### Agent

- `src/Uixe.Copilot.Agent`
- `src/Uixe.Copilot.Agent.Core`
- `libvlc_zip`

#### 前端

- `src/Uixe.Copilot.Web`

#### 测试

- `tests/Uixe.Copilot.Api.Tests`
- `tests/Uixe.Copilot.Application.Tests`

当前说明：

- `Uixe.Copilot.Api` 已提供健康检查、系统配置与交通事件相关接口骨架
- `Uixe.Copilot.Api` 已新增旧 `LaneController` 路由接管控制器，开始承接车道状态、TCO、普通消息、超限告警、特情、大件运输、发票、入口确认与旧 `TrafficEvent` 兼容入口
- 简单展示类旧 DTO 已开始迁入 `Uixe.Copilot.Contracts`，当前车道状态、普通消息、超限告警、车道特情链路已改为新契约强类型传递
- `Uixe.Copilot.Web` 已提供深色监控首页、事件中心、详情页，并已开始对接真实后端接口
- `Uixe.Copilot.Agent` 已提供跨平台宿主骨架，并通过 `Core + 单宿主内平台适配` 分层封装托盘、通知、语音、VNC 与 WebView 能力接口；当前宿主按桌面 GUI 方式构建，支持在 Windows 和 Linux 下以双击方式启动
- `libvlc_zip` 现阶段已明确作为 Agent 侧本地视频播放能力依赖，用于承接旧本地媒体播放场景
- `Uixe.Copilot.Agent` 已内置本地 HTTP 指令服务，可接收并执行通知、语音播报、VNC 打开和 Web 地址打开等命令
- `Uixe.Copilot.Agent` 启动时会向 `Agent:LaneBossServer` 配置的内网服务发起 `POST /guesswhoiam`，把当前收费站识别结果缓存在 Agent 内存中；Debug 构建下可通过 `Agent:ForceLocalhostInDebugBuild=true` 将返回的主机地址统一改写为 `localhost`
- `Uixe.Copilot.Web` 已新增 `agentApi.ts`，并在系统配置页接入本地 Agent 调试入口，可直接通过 `POST http://127.0.0.1:17173/commands` 调用通知、语音和 VNC 能力
- `Uixe.Copilot.Web` 当前前端工具链已明确要求 Node.js LTS `22.12.0+`，并通过 `.nvmrc`、`.node-version`、`package.json engines` 与 `Dockerfile` 统一宿主机和容器环境基线
- 旧 `Uixe.Watcher` 当前仅作为迁移参考源码，不再作为正式兼容运行目标

Agent 本地 HTTP 服务当前默认监听 `http://127.0.0.1:17173/commands`，`Uixe.Copilot.Web` 或其他本机业务可直接以 `POST` JSON 调用，例如：

- 弹窗通知：`{"commandType":"notification","title":"交通事件","message":"X02 车道发现异常车辆"}`
- 弹窗并播报：`{"commandType":"notification","title":"超限提醒","message":"请处理超限车辆","playSpeech":true}`
- 语音播报：`{"commandType":"speech","text":"X02 车道发现异常事件","voiceName":"Microsoft Xiaoxiao Desktop"}`
- 打开 VNC：`{"commandType":"vnc","host":"192.168.1.10","port":5900,"password":"kissme","vncTitle":"X02 车道远程桌面"}`
- 打开 Web：`{"commandType":"web","url":"http://127.0.0.1:5173","webTitle":"Uixe.Copilot"}`

### Agent 发布说明

- `src/Uixe.Copilot.Agent` 当前已收敛为单目标框架 `net10.0`，平台能力通过运行时判断 `OperatingSystem.IsWindows()` / `OperatingSystem.IsLinux()` 选择注册，而不是通过条件编译区分。
- GitHub Actions 发布工作流位于 `.github/workflows/publish-agent.yml`，支持 `workflow_dispatch` 手动触发和 `release.published` 自动触发。
- 当前发布产物为 3 个 Native AOT 包：
  - `uixe-copilot-agent-win-x64.zip`
  - `uixe-copilot-agent-ubuntu-22.04-x64.tar.gz`
  - `uixe-copilot-agent-centos-stream-9-x64.tar.gz`
- Windows 侧不再发布 `net10.0-windows7.0` 或面向 Windows 7 的包，因为 `.NET 10` 已不支持 Windows 7；当前统一发布 `win-x64` AOT 产物。
- Linux 侧按 `.NET 10` 支持基线分别在 `Ubuntu 22.04` 与 `CentOS Stream 9` 环境内进行 `linux-x64` AOT 发布，以降低运行时兼容性风险。
- Linux AOT 依赖 `clang` 和 `zlib` 开发包，工作流已在容器内安装；如果本地手动发布，也需要先准备对应依赖。

本地手动发布可参考：

- Windows：`dotnet publish src/Uixe.Copilot.Agent/Uixe.Copilot.Agent.csproj -c Release -r win-x64 --self-contained true -p:PublishAot=true`
- Linux：`dotnet publish src/Uixe.Copilot.Agent/Uixe.Copilot.Agent.csproj -c Release -r linux-x64 --self-contained true -p:PublishAot=true`

#### 可选本地能力扩展

- `Uixe.Copilot.Agent`
- 本地通知
- 本地语音播报
- 本地 `VNC` 调起
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

## 四、当前实施原则

### 1. 以新系统接管旧系统，不再以兼容旧宿主为目标

现阶段重点不是继续维持旧 `Watcher` 运行，而是按功能清单完成接管。当前优先接管以下入口：

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

### 4. 旧 `WinForms` 仅作为迁移参考源码

后续 `WinForms` 不再作为正式交付目标：

- 不再新增功能
- 不再作为兼容壳层继续演进
- 仅用于对照旧逻辑、旧页面和旧交互
- 待迁移完成后可进一步归档或移出主线

### 5. 先接管路由，再逐步去旧化

当前后端接管策略分两步推进：

- 先由 `Uixe.Copilot.Api` 接管旧接口路由和调用入口
- 再逐步把旧 `Uixe.Watcher` 中的 DTO、桥接与服务实现迁移到 `Contracts`、`Application`、`Infrastructure`

因此当前阶段允许新 API 通过项目引用短期复用旧模型，但这只是过渡方案，不是最终形态。

当前已完成的第一轮去旧化范围：

- `LaneStatus -> LaneStatusDto`
- `MsgInfo -> LaneMessageDto`
- `OverloadWarning -> OverloadWarningDto`
- `Lanespecial -> LaneSpecialDto`

当前已完成的第二轮去旧化范围：

- `BulklyDto -> BulkTransportDto`
- `BillInfoDto -> BillInfoRequestDto`
- `ConfirmEnInfo -> ConfirmEnInfoDto`

下一轮将继续处理：

- 旧 `LegacyTcoInteractionService` 内部桥接
- 旧 TCO WinForms 交互适配层

当前已完成的第三轮去旧化范围：

- `TCOCall -> TcoConfirmRequestDto`
- `MsgWeightTCOCALL -> TcoWeightMessageDto`
- `WATCHER_TYPE -> WatcherType`
- `DlgType -> TcoDialogType`
- `MsgTcoTran -> TcoTranDto`

当前已完成的第四轮去旧化范围：

- `LegacyTcoInteractionService` 改为接收 `TcoWeightMessageDto` / `TcoConfirmRequestDto`
- TCO 桥接层内部再转换为旧 WinForms 模型，而不是让应用层继续直接传旧对象

当前已完成的第五轮去旧化范围：

- `Uixe.Copilot.Api/LaneController` 中简单请求模型、业务请求模型、TCO 请求模型已全部切换为 `Contracts` DTO 入参
- 旧 `TrafficEventPushRequest` / `TrafficEventPushResponse` 兼容入口已改为使用 `Contracts` 请求 DTO 和新的兼容响应 DTO

当前已完成的第六轮依赖收缩范围：

- `Contracts` 层中依赖旧 `Uixe.Watcher` 类型的兼容映射已迁回 `Uixe.Watcher` 最内层桥接实现
- `Uixe.Copilot.Api.csproj` 已移除对 `Uixe.Watcher.csproj` 的直接项目引用
- `libvlc_zip` 当前归属已明确收口到 Agent 侧，用于本地视频播放能力

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

## 六、实施路线图与当前进展

动态进展、完成百分比、已完成 / 进行中 / 尚未完成项、优先改造文件与下一步计划，已迁移到独立路线图文档：

- `roadmap.md`

说明：

- `README.md` 仅保留项目介绍、技术框架、长期约束、目录建议和实施方法。
- 当前推进状态与每轮动态变化统一在路线图文档中维护。

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

- `PostgreSQL`：作为当前项目唯一目标数据库，承接事件类数据存储与历史查询

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
