# Uixe.Copilot Web

基于仓库 `README.md` 中的前端规划新增的 Web UI 一期工程。

## 技术栈

- Vue 3
- TypeScript
- Vite
- Pinia
- Vue Router
- Arco Design Vue
- Tailwind CSS
- ECharts
- SignalR Client

## 当前页面

- 监控总览
- 收费站监控
- 事件中心
- 事件详情
- 历史查询
- 系统配置

## 当前状态

当前已完成：

- 深色科技风主布局
- 总览首页指标卡与趋势图
- 收费站监控页
- 事件中心页
- 事件详情页
- 基础路由与 Pinia 状态
- 对接 `Uixe.Copilot.Api`

当前已接入接口：

- `GET /api/health`
- `GET /api/traffic-events/overview`
- `GET /api/traffic-events/{eventId}`
- `POST /api/traffic-events`
- `SignalR /hubs/traffic-events`

当前实时能力：

- 前端已接入 SignalR 客户端骨架
- 提交 `TrafficEvent` 成功后，后端会向 `trafficEventSubmitted` 广播
- 前端收到广播后会自动刷新总览数据

当前本地 Agent 联动能力：

- 已新增 `src/api/agentApi.ts`，封装本机 `POST http://127.0.0.1:17173/commands` 调用
- 已在系统配置页增加本地 Agent 调试入口，可直接触发：
	- 本地告警通知
	- 本地语音播报
	- 本地 VNC 打开
- 这部分能力用于逐步替代旧 `WinForms` 中的告警、语音、VNC 本地能力入口

## 启动方式

### 1. 启动后端

在仓库根目录运行：

`dotnet run --project src/Uixe.Copilot.Api/Uixe.Copilot.Api.csproj`

默认前端按 `http://localhost:5057` 访问后端。

### 2. 启动前端

在 `src/Uixe.Copilot.Web` 目录运行：

`npm install`

`npm run dev`

### 3. 自定义后端地址

可通过环境变量指定：

- `VITE_API_BASE_URL`

例如指向其他 API 地址。

### 4. 自定义本地 Agent 地址

可通过环境变量指定：

- `VITE_AGENT_BASE_URL`

默认值为：

- `http://127.0.0.1:17173`

## 下一步

- 将更多旧本地按钮逐步切到 `agentApi.ts`
- 对接真实 `TrafficEvent` 数据链路
- 补齐更多 SignalR 实时消息类型
- 增加历史查询与系统配置真实接口
- 增加媒体预览，图片使用 `ImageBox`、视频使用 `VideoView` 对应承载方案
