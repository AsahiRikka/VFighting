@echo 处理中.....
@echo off
for /d /r %1 %%A in (.) do (
  dir /a /b "%%~fA" 2>nul | findstr "^" >nul || echo >%%~fA\.gitignore
)
@echo 处理完毕
pause