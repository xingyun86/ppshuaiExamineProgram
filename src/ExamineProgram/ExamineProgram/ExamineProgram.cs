using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace ExamineProgram
{
    class ExamineProgram : Window
    {
        private Grid grid = new Grid();
        private Button btnLast = new Button();
        private Button btnNext = new Button();
        private Label labelTitle = new Label();
        private Label labelSubTitle = new Label();
        private List<RadioButton> radioButtonList = new List<RadioButton>();
        private DataTable dataTable = null;
        private int rowIndex = 0;
        private const int nItemFixed = 3;
        public ExamineProgram(DataTable dt)
        {
            this.MinWidth = this.MaxWidth = 600;
            this.MinHeight = this.MaxHeight = 500;
            this.ResizeMode = ResizeMode.CanMinimize;
            this.Title = "考试系统";
            dataTable = dt;
            init();
        }
        private void setGridPosition(ContentControl cc, int row, int col)
        {
            cc.SetValue(Grid.RowProperty, row); cc.SetValue(Grid.ColumnProperty, col);
        }
        private void init()
        {
            int nItemsCount = dataTable.Columns.Count - nItemFixed;
            var nNum = nItemFixed + nItemsCount;
            var nRow = 0;
            var nCol = 0;

            rowIndex = 0;

            AddChild(grid);

            for (var n = 0; n < nNum; n++)
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
            }
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });

            btnLast.Content = "上一题";
            btnLast.Margin = new Thickness(20);
            btnLast.Click += BtnLast_Click;
            btnLast.HorizontalAlignment = HorizontalAlignment.Center;

            btnNext.Content = "下一题";
            btnNext.Margin = new Thickness(20);
            btnNext.Click += BtnNext_Click;
            btnNext.HorizontalAlignment = HorizontalAlignment.Center;

            var rows = dataTable.Rows;
            string title = "";
            string subtitle = "";
            List<string> contentlist = new List<string>();
            if (rows.Count > 0)
            {
                title = "";
                if (rows[rowIndex][1].ToString() == "")
                {
                    title = rows[rowIndex][0].ToString();
                    if (rows[rowIndex + 1][6].ToString() != "")
                    {
                        subtitle = rows[rowIndex + 1][0].ToString() + rows[rowIndex + 1][1].ToString();
                        contentlist.Add(rows[rowIndex + 1][2].ToString());
                        contentlist.Add(rows[rowIndex + 1][3].ToString());
                        contentlist.Add(rows[rowIndex + 1][4].ToString());
                        contentlist.Add(rows[rowIndex + 1][5].ToString());
                    }
                    else if (rows[rowIndex+1][5].ToString() != "")
                    {
                        subtitle = rows[rowIndex + 1][0].ToString() + rows[rowIndex + 1][1].ToString();
                        contentlist.Add(rows[rowIndex + 1][2].ToString());
                        contentlist.Add(rows[rowIndex + 1][3].ToString());
                        contentlist.Add(rows[rowIndex + 1][4].ToString());
                    }
                    else if (rows[rowIndex+1][4].ToString() != "")
                    {
                        subtitle = rows[rowIndex + 1][0].ToString() + rows[rowIndex + 1][1].ToString();
                        contentlist.Add(rows[rowIndex + 1][2].ToString());
                        contentlist.Add(rows[rowIndex + 1][3].ToString());
                    }
                    rowIndex += 2;
                }
                else
                {
                    if (rows[rowIndex][6].ToString() != "")
                    {
                        subtitle = rows[rowIndex][0].ToString() + rows[rowIndex][1].ToString();
                        contentlist.Add(rows[rowIndex][2].ToString());
                        contentlist.Add(rows[rowIndex][3].ToString());
                        contentlist.Add(rows[rowIndex][4].ToString());
                        contentlist.Add(rows[rowIndex][5].ToString());
                    }
                    else if (rows[rowIndex][5].ToString() != "")
                    {
                        subtitle = rows[rowIndex][0].ToString() + rows[rowIndex][1].ToString();
                        contentlist.Add(rows[rowIndex][2].ToString());
                        contentlist.Add(rows[rowIndex][3].ToString());
                        contentlist.Add(rows[rowIndex][4].ToString());
                    }
                    else if (rows[rowIndex][4].ToString() != "")
                    {
                        subtitle = rows[rowIndex][0].ToString() + rows[rowIndex][1].ToString();
                        contentlist.Add(rows[rowIndex][2].ToString());
                        contentlist.Add(rows[rowIndex][3].ToString());
                    }
                    rowIndex+=1;
                }
            }

            labelTitle.Content = title;
            labelTitle.Margin = new Thickness(20);
            labelTitle.HorizontalAlignment = HorizontalAlignment.Left;

            labelSubTitle.Content = subtitle;
            labelSubTitle.Margin = new Thickness(20);
            labelSubTitle.HorizontalAlignment = HorizontalAlignment.Left;

            for (var n = 0; n < nItemsCount; n++)
            {
                var radioBtnItem = new RadioButton();
                string text = "";
                if(contentlist.Count > n)
                {
                    text = contentlist[n];
                }
                else
                {
                    radioBtnItem.IsEnabled = false;
                }
                radioBtnItem.Content = ((char)('A' + n)).ToString() + "." + text;
                radioBtnItem.Margin = new Thickness(20);
                radioBtnItem.HorizontalAlignment = HorizontalAlignment.Left;
                radioButtonList.Add(radioBtnItem);
            }
            nRow = 0;
            nCol = 0;
            setGridPosition(btnLast, nRow, nCol);
            nCol = 2;
            setGridPosition(btnNext, nRow++, nCol); 
            nCol = 1;
            setGridPosition(labelTitle, nRow++, nCol);
            setGridPosition(labelSubTitle, nRow++, nCol);
            foreach (var it in radioButtonList)
            {
                setGridPosition(it, nRow++, nCol);
            }

            grid.Children.Add(btnLast);
            grid.Children.Add(btnNext);
            grid.Children.Add(labelTitle);
            grid.Children.Add(labelSubTitle);

            foreach(var it in radioButtonList)
            {
                grid.Children.Add(it);
            }
        }

        private void BtnLast_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            var rows = dataTable.Rows;
            string title = "";
            string subtitle = "";
            List<string> contentlist = new List<string>();
            if(rowIndex > 0)
            {
                rowIndex--;
            }
            if (rows.Count > 0 && rowIndex < rows.Count)
            {
                title = "";
                if (rows[rowIndex][1].ToString() == "")
                {
                    title = rows[rowIndex][0].ToString();
                    if (rows[rowIndex + 1][6].ToString() != "")
                    {
                        subtitle = rows[rowIndex + 1][0].ToString() + rows[rowIndex + 1][1].ToString();
                        contentlist.Add(rows[rowIndex + 1][2].ToString());
                        contentlist.Add(rows[rowIndex + 1][3].ToString());
                        contentlist.Add(rows[rowIndex + 1][4].ToString());
                        contentlist.Add(rows[rowIndex + 1][5].ToString());
                    }
                    else if (rows[rowIndex + 1][5].ToString() != "")
                    {
                        subtitle = rows[rowIndex + 1][0].ToString() + rows[rowIndex + 1][1].ToString();
                        contentlist.Add(rows[rowIndex + 1][2].ToString());
                        contentlist.Add(rows[rowIndex + 1][3].ToString());
                        contentlist.Add(rows[rowIndex + 1][4].ToString());
                    }
                    else if (rows[rowIndex + 1][4].ToString() != "")
                    {
                        subtitle = rows[rowIndex + 1][0].ToString() + rows[rowIndex + 1][1].ToString();
                        contentlist.Add(rows[rowIndex + 1][2].ToString());
                        contentlist.Add(rows[rowIndex + 1][3].ToString());
                    }
                    //rowIndex += 2;
                }
                else
                {
                    if (rows[rowIndex][6].ToString() != "")
                    {
                        subtitle = rows[rowIndex][0].ToString() + rows[rowIndex][1].ToString();
                        contentlist.Add(rows[rowIndex][2].ToString());
                        contentlist.Add(rows[rowIndex][3].ToString());
                        contentlist.Add(rows[rowIndex][4].ToString());
                        contentlist.Add(rows[rowIndex][5].ToString());
                    }
                    else if (rows[rowIndex][5].ToString() != "")
                    {
                        subtitle = rows[rowIndex][0].ToString() + rows[rowIndex][1].ToString();
                        contentlist.Add(rows[rowIndex][2].ToString());
                        contentlist.Add(rows[rowIndex][3].ToString());
                        contentlist.Add(rows[rowIndex][4].ToString());
                    }
                    else if (rows[rowIndex][4].ToString() != "")
                    {
                        subtitle = rows[rowIndex][0].ToString() + rows[rowIndex][1].ToString();
                        contentlist.Add(rows[rowIndex][2].ToString());
                        contentlist.Add(rows[rowIndex][3].ToString());
                    }
                    //rowIndex += 1;
                }
            }
            labelTitle.Content = title;
            labelSubTitle.Content = subtitle;
            for (var n = 0; n < radioButtonList.Count; n++)
            {
                var radioBtnItem = radioButtonList[n];
                string text = "";
                if (contentlist.Count > n)
                {
                    text = contentlist[n];
                    radioBtnItem.Content = ((char)('A' + n)).ToString() + "." + text;
                    radioBtnItem.Visibility = Visibility.Visible;
                }
                else
                {
                    radioBtnItem.Visibility = Visibility.Hidden;
                }
            }
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            var rows = dataTable.Rows;
            string title = "";
            string subtitle = "";
            List<string> contentlist = new List<string>();
            if (rows.Count > 0 && rowIndex < rows.Count)
            {
                title = "";
                if (rows[rowIndex][1].ToString() == "")
                {
                    title = rows[rowIndex][0].ToString();
                    if (rows[rowIndex + 1][6].ToString() != "")
                    {
                        subtitle = rows[rowIndex + 1][0].ToString() + rows[rowIndex + 1][1].ToString();
                        contentlist.Add(rows[rowIndex + 1][2].ToString());
                        contentlist.Add(rows[rowIndex + 1][3].ToString());
                        contentlist.Add(rows[rowIndex + 1][4].ToString());
                        contentlist.Add(rows[rowIndex + 1][5].ToString());
                    }
                    else if (rows[rowIndex+1][5].ToString() != "")
                    {
                        subtitle = rows[rowIndex + 1][0].ToString() + rows[rowIndex + 1][1].ToString();
                        contentlist.Add(rows[rowIndex + 1][2].ToString());
                        contentlist.Add(rows[rowIndex + 1][3].ToString());
                        contentlist.Add(rows[rowIndex + 1][4].ToString());
                    }
                    else if (rows[rowIndex+1][4].ToString() != "")
                    {
                        subtitle = rows[rowIndex + 1][0].ToString() + rows[rowIndex + 1][1].ToString();
                        contentlist.Add(rows[rowIndex + 1][2].ToString());
                        contentlist.Add(rows[rowIndex + 1][3].ToString());
                    }
                    rowIndex += 2;
                }
                else
                {
                    if (rows[rowIndex][6].ToString() != "")
                    {
                        subtitle = rows[rowIndex][0].ToString() + rows[rowIndex][1].ToString();
                        contentlist.Add(rows[rowIndex][2].ToString());
                        contentlist.Add(rows[rowIndex][3].ToString());
                        contentlist.Add(rows[rowIndex][4].ToString());
                        contentlist.Add(rows[rowIndex][5].ToString());
                    }
                    else if (rows[rowIndex][5].ToString() != "")
                    {
                        subtitle = rows[rowIndex][0].ToString() + rows[rowIndex][1].ToString();
                        contentlist.Add(rows[rowIndex][2].ToString());
                        contentlist.Add(rows[rowIndex][3].ToString());
                        contentlist.Add(rows[rowIndex][4].ToString());
                    }
                    else if (rows[rowIndex][4].ToString() != "")
                    {
                        subtitle = rows[rowIndex][0].ToString() + rows[rowIndex][1].ToString();
                        contentlist.Add(rows[rowIndex][2].ToString());
                        contentlist.Add(rows[rowIndex][3].ToString());
                    }
                    rowIndex += 1;
                }
            }
            labelTitle.Content = title;
            labelSubTitle.Content = subtitle;
            for (var n = 0; n < radioButtonList.Count; n++)
            {
                var radioBtnItem = radioButtonList[n];
                string text = "";
                if (contentlist.Count > n)
                {
                    text = contentlist[n];
                    radioBtnItem.Content = ((char)('A' + n)).ToString() + "." + text;
                    radioBtnItem.Visibility = Visibility.Visible;
                }
                else
                {
                    radioBtnItem.Visibility = Visibility.Hidden;
                }
            }
        }
    }
}
