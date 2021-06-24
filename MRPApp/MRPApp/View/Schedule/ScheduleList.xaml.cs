using MRPAPP.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MRPApp.View.Schedule
{
    /// <summary>
    /// MyAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ScheduleList : Page
    {
        public ScheduleList()
        {
            InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadGridData();

                InitErrorMessages();
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 StoreList Loaded : {ex}");
                throw ex;
            }
        }

        // 텍스트박스 옆 에러메세지 숨기는 함수
        private void InitErrorMessages()
        {
            LblBasicCode.Visibility = LblCodeName.Visibility = LblCodeDesc.Visibility = Visibility.Hidden;
        }

        // 그리드에 DB데이터 읽어오는 함수
        private void LoadGridData()
        {
            List<MRPAPP.Model.Settings> settings = Logic.DataAccess.GetSettings();
            this.DataContext = settings;
        }

        private async void BtnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidInputs() != true) return;

            var setting = new MRPAPP.Model.Settings();
            setting.BasicCode = TxtBasicCode.Text;
            setting.CodeName = TxtCodeName.Text;
            setting.CodeDesc = TxtCodeDesc.Text;
            setting.RegDate = DateTime.Now;
            setting.RegID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSettings(setting);
                if (result == 0) // 실패했으면,,, 
                {
                    Commons.LOGGER.Error("데이터 입력 오류 발생");
                    await Commons.ShowMessageAsync("오류", "데이터 입력 실패!");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 입력 성공 : {setting.BasicCode}"); //로그
                    ClearInputs();
                    LoadGridData();
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 {ex}");
            }
        }

        // 입력 데이터 검증 메서드
        public bool IsValidInputs()     // 테스트를 할때는 public으로 되어있어야 테스트 가능
        {
            var isValid = true;
            InitErrorMessages();

            if (string.IsNullOrEmpty(TxtBasicCode.Text))
            {
                LblBasicCode.Visibility = Visibility.Visible;
                LblBasicCode.Text = "코드를 입력하세요";
                isValid = false;
            }
            else if (Logic.DataAccess.GetSettings().Where(s =>s.BasicCode.Equals(TxtBasicCode.Text)).Count() > 0)
            {
                LblBasicCode.Visibility = Visibility.Visible;
                LblBasicCode.Text = "중복 코드가 존재합니다";
                isValid = false;
            }

            if (string.IsNullOrEmpty(TxtCodeName.Text))
            {
                LblCodeName.Visibility = Visibility.Visible;
                LblCodeName.Text = "코드명을 입력하세요";
                isValid = false;
            }

            return isValid;
        }

        private async void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var setting = GrdData.SelectedItem as MRPAPP.Model.Settings;
            setting.CodeName = TxtCodeName.Text;
            setting.CodeDesc = TxtCodeDesc.Text;
            setting.ModDate = DateTime.Now;
            setting.ModID = "MRP";

            try
            {
                var result = Logic.DataAccess.SetSettings(setting);
                if (result == 0) // 실패했으면,,, 
                {
                    Commons.LOGGER.Error("데이터 수정 오류 발생");
                    await Commons.ShowMessageAsync("오류", "데이터 수정 실패!");
                }
                else
                {
                    Commons.LOGGER.Info($"데이터 수정 성공 : {setting.BasicCode}"); //로그
                    ClearInputs();
                    LoadGridData();
                }
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 {ex}");
            }
        }

        // 텍스트박스 글 모두 지우고 입력가능하게 하는 메서드 
        private void ClearInputs()
        {
            TxtBasicCode.IsReadOnly = false;
            TxtBasicCode.Background = new SolidColorBrush(Colors.White);

            TxtBasicCode.Text = TxtCodeName.Text = TxtCodeDesc.Text = string.Empty;
            TxtBasicCode.Focus();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = TxtSearch.Text.Trim();
            var settings = Logic.DataAccess.GetSettings().Where(s => s.CodeName.Contains(search)).ToList();
            this.DataContext = settings;
        }

        private void GrdData_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                var setting = GrdData.SelectedItem as MRPAPP.Model.Settings;
                TxtBasicCode.Text = setting.BasicCode;
                TxtCodeName.Text = setting.CodeName;
                TxtCodeDesc.Text = setting.CodeDesc;

                TxtBasicCode.IsReadOnly = true;
                TxtBasicCode.Background = new SolidColorBrush(Colors.LightGray);
            }
            catch (Exception ex)
            {
                Commons.LOGGER.Error($"예외발생 {ex}");
                ClearInputs();
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var setting = GrdData.SelectedItem as MRPAPP.Model.Settings;

            if (setting == null)
            {
                await Commons.ShowMessageAsync("삭제", "삭제할 코드를 선택하세요");
                return;
            }
            else
            {
                try
                {
                    var result = Logic.DataAccess.DelSettings(setting);
                    if (result == 0) // 실패했으면,,, 
                    {
                        Commons.LOGGER.Error("데이터 삭제 오류 발생");
                        await Commons.ShowMessageAsync("오류", "데이터 삭제 실패!");
                    }
                    else
                    {
                        Commons.LOGGER.Info($"데이터 삭제 성공 : {setting.BasicCode}"); //로그
                        ClearInputs();
                        LoadGridData();
                    }
                }
                catch (Exception ex)
                {
                    Commons.LOGGER.Error($"예외발생 {ex}");
                }
            }

        }

        // 엔터눌리면 검색하게 하는 이벤트 
        private void TxtSearch_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)  // enter눌리면 검색 !
                BtnSearch_Click(sender, e);
        }
    
    }
}