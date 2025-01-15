# RoboCopy GUI

robocopy指令生成器，包含了个人常用功能选项

运行环境：.NET Framework 4.7.2

## Robocopy介绍：

[Robocopy](https://learn.microsoft.com/en-us/windows-server/administration/windows-commands/robocopy)（全称 Robust File Copy，意为「可靠文件复制工具」）是 Windows 系统自带的**命令行工具**，专为高效复制文件和文件夹设计。它能在不同目录、存储设备（如 U 盘、移动硬盘），甚至网络共享之间进行文件传输。

「文件资源管理器」的复制功能就是一个「黑盒子」，而且功能相对简单，只能查看进度和进行基本的暂停或中止操作；而 Robocopy 提供了超多控制选项，适用于各种复杂场景：

- **自动化流程**：便于实现任务自动化，减少手动操作，显著提升效率。
- **大批量文件传输**：在处理大量文件，尤其是众多小文件时，Robocopy 的多线程复制可以显著加快传输，效率要远超 Windows 文件资源管理器。
- **网络文件同步**：在网络环境下，Robocopy 可以高效同步本地和网络共享中的文件，并支持断点续传和失败重传等功能。
- **增量备份**：支持只复制新增或修改的文件，非常适合增量备份。
- **保留文件属性**：在复制时，可以保留文件的创建时间、修改时间等原始属性。
- **文件筛选**：支持根据文件大小、日期、属性等进行筛选。
- **日志记录**：Robocopy 能生成详细的日志，方便问题排查和审计。

[robocopy指令官方文档](https://learn.microsoft.com/zh-cn/windows-server/administration/windows-commands/robocopy)（沟槽的官方AI翻译）