using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace RoboCopyGUI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region 选择文件/文件夹

        private void ChooseFolder(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = true,
                Title = "请选择路径",
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // 根据控件名称获取控件对象
                var path = dialog.FileName;
                var name = (sender as Button).Tag.ToString();
                if (FindName(name) is TextBox textBox)
                {
                    textBox.Text = path;
                }
            }
        }

        private void ChooseFile(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog()
            {
                IsFolderPicker = false,
                Title = "请选择文件",
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // 根据控件名称获取控件对象
                var path = dialog.FileName;
                var name = (sender as Button).Tag.ToString();
                if (FindName(name) is TextBox textBox)
                {
                    textBox.Text = path;
                }
            }
        }
        #endregion

        #region 生成指令
        private void BuildCommand(object sender, EventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("robocopy ");
            builder.Append(srcDir.Text + " ");
            builder.Append(dstDir.Text + " ");
            builder.Append(filter.Text + " ");
            builder.Append((bool)bCopyChildFolderExceptEmpty.IsChecked ?  "/S " : string.Empty);
            builder.Append((bool)bCopyChildFolderIncludeEmpty.IsChecked ? "/E " : string.Empty);
            builder.Append((bool)bMirrorSrcDir.IsChecked ? "/MIR " : string.Empty);
            builder.Append((bool)bDelMovedFile.IsChecked ? "/MOVE " : string.Empty);
            builder.Append((bool)bMultyThread.IsChecked ?   $"/MT:{threadNum.Text} " : string.Empty);
            builder.Append((bool)bOnlyListFiles.IsChecked ? "/L " : string.Empty);
            builder.Append((bool)bOnlyCopyNewer.IsChecked ? "/XO " : string.Empty);

            command.Text = builder.ToString();
        }

        #endregion

        #region 复制/运行命令 

        private bool CheckCommand()
        {
            if (string.IsNullOrEmpty(srcDir.Text) || string.IsNullOrEmpty(dstDir.Text))
            {
                MessageBox.Show("请选择源目录和目标目录", "错误",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            // 判断threadNum.Text是否为正整数
            if (bMultyThread.IsChecked == true && !int.TryParse(threadNum.Text, out int num)
                && (1 > num || num > 128))
            {
                MessageBox.Show("线程数必须为1~128之间的整数", "错误",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void CopyCommand(object sender, RoutedEventArgs e)
        {
            if (CheckCommand())
                // 复制command.Text
                Clipboard.SetText(command.Text);
        }

        private void RunCommand(object sender, RoutedEventArgs e)
        {
            if (CheckCommand())
            {
                try
                {
                    // 运行command.Text
                    var process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/k " + command.Text; // 使用 /k 而不是 /c
                    process.StartInfo.UseShellExecute = true; // 必须设置为 true 以打开新窗口
                    process.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"运行命令时发生错误: {ex.Message}", "错误",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void RunCommandAsAdmin(object sender, RoutedEventArgs e)
        {
            if (CheckCommand())
            {
                try
                {
                    // 以管理员权限运行command.Text
                    var process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = "/k " + command.Text; // 使用 /k 而不是 /c
                    process.StartInfo.UseShellExecute = true; // 必须设置为 true 以打开新窗口
                    process.StartInfo.Verb = "runas"; // 以管理员权限运行
                    process.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"以管理员权限运行命令时发生错误: {ex.Message}", "错误",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        #endregion
    }
}
