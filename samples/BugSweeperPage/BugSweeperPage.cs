#define FIX_WINPHONE_BUTTON         // IsEnabled=false doesn't disable button

using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BugSweeperPage
{
    public class BugsPage : ContentPage
    {
        const string timeFormat = @"%m\:ss";

        bool isGameInProgress;

        DateTime gameStartTime;

        string xaml = @"
            <ContentPage xmlns=""http://xamarin.com/schemas/2014/forms"" xmlns:x=""http://schemas.microsoft.com/winfx/2009/xaml"" xmlns:local=""clr-namespace:BugSweeperPage"" x:Class=""BugSweeperPage.BugsPage"" Title=""BugSweeper"">
                <ContentPage.Padding><OnPlatform x:TypeArguments=""Thickness"" iOS=""0,20,0,0"" Android=""0,0,0,0"" WinPhone=""0,0,0,0"" /></ContentPage.Padding>
                <ContentView SizeChanged=""OnMainContentViewSizeChanged"">
                    <Grid x:Name=""mainGrid"" ColumnSpacing=""0"" RowSpacing=""0"">
                    <Grid.RowDefinitions><RowDefinition Height=""7*"" /><RowDefinition Height=""4*"" /></Grid.RowDefinitions>
                    <Grid.ColumnDefinitions><ColumnDefinition Width=""0"" /><ColumnDefinition Width=""*"" /></Grid.ColumnDefinitions>
                        <StackLayout x:Name=""textStack"" Grid.Row=""0"" Grid.Column=""1"" Spacing=""0"">
                            <StackLayout HorizontalOptions=""Center"" Spacing=""0"">
                                <Label Text=""BugSweeper"" Font=""Bold, Large"" TextColor=""Accent"" />
                                <BoxView Color=""Accent"" HeightRequest=""3"" />
                            </StackLayout>
                            <Label Text=""Tap to flag/unflag a potential bug."" VerticalOptions=""CenterAndExpand"" HorizontalTextAlignment=""Center"" />
                            <Label Text=""Double-tap if you're sure it's not a bug.&#xA;The first double-tap is always safe!"" VerticalOptions=""CenterAndExpand"" HorizontalTextAlignment=""Center"" />
                            <StackLayout Orientation=""Horizontal"" Spacing=""0"" VerticalOptions=""CenterAndExpand"" HorizontalOptions=""Center"">
                                <Label BindingContext=""{x:Reference board}"" Text=""{Binding FlaggedTileCount, StringFormat='Flagged {0} '}"" />
                                <Label BindingContext=""{x:Reference board}"" Text=""{Binding BugCount, StringFormat=' out of {0} bugs.'}"" />
                            </StackLayout>
                            <Label x:Name=""timeLabel"" Text=""0:00"" VerticalOptions=""CenterAndExpand"" HorizontalTextAlignment=""Center"" />
                        </StackLayout>
                        <ContentView Grid.Row=""1"" Grid.Column=""1"" SizeChanged=""OnBoardContentViewSizeChanged"">
                            <Grid>
                                <Grid.RowDefinitions>
                                    <RowDefinition Height=""*"" />
                                </Grid.RowDefinitions>
                                <Grid.ColumnDefinitions>
                                    <ColumnDefinition Width=""*"" />
                                </Grid.ColumnDefinitions>
                                <local:Board x:Name=""board"" />
                                <StackLayout x:Name=""congratulationsText"" Orientation=""Horizontal"" HorizontalOptions=""Center"" VerticalOptions=""Center"" Spacing=""0"">
                                    <Label Text=""C"" TextColor=""Red"" />
                                    <Label Text=""O"" TextColor=""Red"" />
                                    <Label Text=""N"" TextColor=""Red"" />
                                    <Label Text=""G"" TextColor=""Red"" />
                                    <Label Text=""R"" TextColor=""Red"" />
                                    <Label Text=""A"" TextColor=""Red"" />
                                    <Label Text=""T"" TextColor=""Red"" />
                                    <Label Text=""U"" TextColor=""Red"" />
                                    <Label Text=""L"" TextColor=""Red"" />
                                    <Label Text=""A"" TextColor=""Red"" />
                                    <Label Text=""T"" TextColor=""Red"" />
                                    <Label Text=""I"" TextColor=""Red"" />
                                    <Label Text=""O"" TextColor=""Red"" />
                                    <Label Text=""N"" TextColor=""Red"" />
                                    <Label Text=""S"" TextColor=""Red"" />
                                    <Label Text=""!"" TextColor=""Red"" />
                                </StackLayout>                                                                           
                                <StackLayout x:Name=""consolationText"" Orientation=""Horizontal"" Spacing=""0"" HorizontalOptions=""Center"" VerticalOptions=""Center"">
                                    <Label Text=""T"" TextColor=""Red"" />
                                    <Label Text=""O"" TextColor=""Red"" />
                                    <Label Text=""O"" TextColor=""Red"" />
                                    <Label Text="" "" TextColor=""Red"" />
                                    <Label Text=""B"" TextColor=""Red"" />
                                    <Label Text=""A"" TextColor=""Red"" />
                                    <Label Text=""D"" TextColor=""Red"" />
                                    <Label Text=""!"" TextColor=""Red"" />
                                </StackLayout>
                                <Button x:Name=""playAgainButton"" Text="" Play Another Game? "" HorizontalOptions=""Center"" VerticalOptions=""Center"" Clicked=""OnplayAgainButtonClicked"" BorderColor=""Black"" BorderWidth=""2"" BackgroundColor=""White"" TextColor=""Black"" />
                            </Grid>
                        </ContentView>
                    </Grid>
                </ContentView>
            </ContentPage>";            

        public BugsPage()
        {
            this.LoadFromXaml<BugsPage>(xaml);
            var board = this.FindByName<Board>("board");
            var timeLabel = this.FindByName<Label>("timeLabel");
            board.GameStarted += (sender, args) =>
            {
                isGameInProgress = true;
                gameStartTime = DateTime.Now;

                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    timeLabel.Text = (DateTime.Now - gameStartTime).ToString(timeFormat);
                    return isGameInProgress;
                });
            };

            board.GameEnded += (sender, hasWon) =>
            {
                isGameInProgress = false;

                if (hasWon)
                    DisplayWonAnimation();
                else
                    DisplayLostAnimation();
            };

            PrepareForNewGame();
        }

        void PrepareForNewGame()
        {
            var board = this.FindByName<Board>("board");
            var timeLabel = this.FindByName<Label>("timeLabel");
            var congratulationsText = this.FindByName<StackLayout>("congratulationsText");
            var consolationText = this.FindByName<StackLayout>("consolationText");
            var playAgainButton = this.FindByName<Button>("playAgainButton");

            board.NewGameInitialize();
            congratulationsText.IsVisible = false;
            consolationText.IsVisible = false;
            playAgainButton.IsVisible = false;
            playAgainButton.IsEnabled = false;

            timeLabel.Text = new TimeSpan().ToString(timeFormat);
            isGameInProgress = false;
        }

        void OnMainContentViewSizeChanged(object sender, EventArgs args)
        {
            ContentView contentView = (ContentView)sender;
            double width = contentView.Width;
            double height = contentView.Height;
            bool isLandscape = width > height;
            var mainGrid = this.FindByName<Grid>("mainGrid");
            var textStack = this.FindByName<StackLayout>("textStack");

            if (isLandscape)
            {
                mainGrid.RowDefinitions[0].Height = 0;
                mainGrid.RowDefinitions[1].Height = new GridLength(1, GridUnitType.Star);
                mainGrid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                Grid.SetRow(textStack, 1);
                Grid.SetColumn(textStack, 0);
            }
            else
            {
                mainGrid.RowDefinitions[0].Height = new GridLength(3, GridUnitType.Star);
                mainGrid.RowDefinitions[1].Height = new GridLength(5, GridUnitType.Star);
                mainGrid.ColumnDefinitions[0].Width = 0;
                mainGrid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                Grid.SetRow(textStack, 0);
                Grid.SetColumn(textStack, 1);
            }
        }

        void OnBoardContentViewSizeChanged(object sender, EventArgs args)
        {
            ContentView contentView = (ContentView)sender;
            double width = contentView.Width;
            double height = contentView.Height;
            double dimension = Math.Min(width, height);
            double horzPadding = (width - dimension) / 2;
            double vertPadding = (height - dimension) / 2;
            contentView.Padding = new Thickness(horzPadding, vertPadding);
        }

        async Task DisplayWonAnimation()
        {
            var board = this.FindByName<Board>("board");
            var congratulationsText = this.FindByName<StackLayout>("congratulationsText");

            congratulationsText.Scale = 0;
            congratulationsText.IsVisible = true;

            // Because IsVisible has been false, the text might not have a size yet, 
            //  in which case Measure will return a size.
            double congratulationsTextWidth = congratulationsText.Measure(Double.PositiveInfinity, Double.PositiveInfinity).Request.Width;

            congratulationsText.Rotation = 0;
            congratulationsText.RotateTo(3 * 360, 1000, Easing.CubicOut);

            double maxScale = 0.9 * board.Width / congratulationsTextWidth;
            await congratulationsText.ScaleTo(maxScale, 1000);

            foreach (View view in congratulationsText.Children)
            {
                view.Rotation = 0;
                view.RotateTo(180);
                await view.ScaleTo(3, 100);
                view.RotateTo(360);
                await view.ScaleTo(1, 100);
            }

            await DisplayPlayAgainButton();
        }

        async Task DisplayLostAnimation()
        {
            var board = this.FindByName<Board>("board");
            var consolationText = this.FindByName<StackLayout>("consolationText");

            consolationText.Scale = 0;
            consolationText.IsVisible = true;

            // (See above for rationale)
            double consolationTextWidth = consolationText.Measure(Double.PositiveInfinity, Double.PositiveInfinity).Request.Width;

            double maxScale = 0.9 * board.Width / consolationTextWidth;
            await consolationText.ScaleTo(maxScale, 1000);
            await Task.Delay(1000);
            await DisplayPlayAgainButton();
        }

        async Task DisplayPlayAgainButton()
        {
            var board = this.FindByName<Board>("board");
            var playAgainButton = this.FindByName<Button>("playAgainButton");
            playAgainButton.Scale = 0;
            playAgainButton.IsVisible = true;
            playAgainButton.IsEnabled = true;

            // (See above for rationale)
            double playAgainButtonWidth = playAgainButton.Measure(Double.PositiveInfinity, Double.PositiveInfinity).Request.Width;

            double maxScale = board.Width / playAgainButtonWidth;
            await playAgainButton.ScaleTo(maxScale, 1000, Easing.SpringOut);
        }

        void OnplayAgainButtonClicked(object sender, object EventArgs)
        {
#if FIX_WINPHONE_BUTTON

            if (Device.RuntimePlatform == Device.UWP && !((Button)sender).IsEnabled)
                return;

#endif
            PrepareForNewGame();
        }
    }
}
