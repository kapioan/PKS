using ExamSQL.Addons;
using ExamSQL.Context;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExamSQL;

public partial class MainViewModel : Window
{
    public MainViewModel()
    {
        InitializeComponent();
    }

    private void Send1_Click(object sender, RoutedEventArgs e)
    {
        string enterData = ProgramEnter1.Text;

        var res = Requester.SendRequest1(enterData);

        string textInput = string.Join("\n", res.Select(x => $"Enrollee's Name: {x.Name}, ID: {x.Id}"));

        Output1.Text = textInput;
    }

    private void Send2_Click(object sender, RoutedEventArgs e)
    {
        string enterData = ProgramEnter2.Text;

        var res = Requester.SendRequest2(enterData);

        string textInput = string.Join("\n", res.Select(x => $"Program's Name: {x.Name}, Department: {x.Department.Name}, Plan: {x.Plan}"));

        Output2.Text = textInput;
    }

    private void Send3_Click(object sender, RoutedEventArgs e)
    {
        // no enter data

        var res = Requester.SendRequest3();

        string textInput = string.Join("\n", res);

        Output3.Text = textInput;
    }

    private void Send4_Click(object sender, RoutedEventArgs e)
    {
        // balls (баллы)
        string enterData = ProgramEnter4.Text;

        var res = Requester.SendRequest4(enterData);

        string textInput = string.Join("\n", res);

        Output4.Text = textInput;
    }

    private void Send5_Click(object sender, RoutedEventArgs e)
    {
        string enterData = ProgramEnter5.Text;

        var res = Requester.SendRequest5(enterData);

        string textInput = string.Join("\n", res);

        Output5.Text = textInput;
    }

    private void Send6_Click(object sender, RoutedEventArgs e)
    {
        var res = Requester.SendRequest6();

        string textInput = string.Join("\n", res);

        Output6.Text = textInput;
    }

    private void Send7_Click(object sender, RoutedEventArgs e)
    {
        var res = Requester.SendRequest7();

        string textInput = string.Join("\n", res);

        Output7.Text = textInput;
    }

    private void Send8_Click(object sender, RoutedEventArgs e)
    {
        string enterData = ProgramEnter8.Text,
               enterData2 = ProgramEnter8_Copy.Text;

        var res = Requester.SendRequest8(enterData, enterData2);

        string textInput = string.Join("\n", res);

        Output8.Text = textInput;
    }

    private void Send9_Click(object sender, RoutedEventArgs e)
    {
        var res = Requester.SendRequest9();

        string textInput = string.Join("\n", res);

        Output9.Text = textInput;
    }

    private void Send10_Click(object sender, RoutedEventArgs e)
    {
        var res = Requester.SendRequest10();

        string textInput = string.Join("\n", res);

        Output10.Text = textInput;
    }
}