# Copilot Instructions

## 项目指南
- For traffic event media previews, use `ImageBox` to display images and `VideoView` to play videos in tab pages.

## 项目实施规则
- Follow the phased modernization path: compatibility first, replacement second, decoupling before migration.
- Do not directly delete existing WinForms content during the first-stage refactor.
- Keep the existing UI and the new Web UI able to coexist and switch during migration.
- Do not introduce new `Controller -> Form` direct dependencies.
- Move business logic from `Controller` and WinForms into application services.
- Keep upstream request contracts stable unless a change is explicitly approved.
- Prefer adding compatibility adapters instead of breaking old interfaces.
- Treat `WinForms` as a display or local capability host, not the long-term business core.
- Target new backend code toward an independent `ASP.NET Core 10 Web API` architecture.