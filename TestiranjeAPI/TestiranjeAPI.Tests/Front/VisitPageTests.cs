using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestiranjeAPI.Tests.Front;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class VisitPageTests : PageTest
{
    [Test]
    public async Task VisitPartiesPage()
    {
        await Page.GotoAsync("http://127.0.0.1:5500/Front/Pages/Login/index.html");
        await Page.GetByLabel("Username").ClickAsync();
        await Page.GetByLabel("Username").FillAsync("pwVisitTest");
        await Page.GetByLabel("Password").ClickAsync();
        await Page.GetByLabel("Password").FillAsync("pwVisitTest123@");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        await Page.GetByRole(AriaRole.Button, new() { Name = "Parties", Exact = true }).ClickAsync();
        await Page.WaitForLoadStateAsync();

        string expectedTitle = "Parties";
        string actualTitle = await Page.TitleAsync();

        Assert.That(actualTitle, Is.EqualTo(expectedTitle));
    }

    [Test]
    public async Task VisitMyPartiesPage()
    {
        await Page.GotoAsync("http://127.0.0.1:5500/Front/Pages/Login/index.html");
        await Page.GetByLabel("Username").ClickAsync();
        await Page.GetByLabel("Username").FillAsync("pwVisitTest");
        await Page.GetByLabel("Password").ClickAsync();
        await Page.GetByLabel("Password").FillAsync("pwVisitTest123@");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        await Page.GetByRole(AriaRole.Button, new() { Name = "My Parties" }).ClickAsync();
        await Page.WaitForLoadStateAsync();

        string expectedTitle = "My Parties";
        string actualTitle = await Page.TitleAsync();

        Assert.That(actualTitle, Is.EqualTo(expectedTitle));
    }

    [Test]
    public async Task VisitCreatePartyPage()
    {
        await Page.GotoAsync("http://127.0.0.1:5500/Front/Pages/Login/index.html");
        await Page.GetByLabel("Username").ClickAsync();
        await Page.GetByLabel("Username").FillAsync("pwVisitTest");
        await Page.GetByLabel("Password").ClickAsync();
        await Page.GetByLabel("Password").FillAsync("pwVisitTest123@");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        await Page.GetByRole(AriaRole.Button, new() { Name = "Create Party" }).ClickAsync();
        await Page.WaitForLoadStateAsync();

        string expectedTitle = "Create Party";
        string actualTitle = await Page.TitleAsync();

        Assert.That(actualTitle, Is.EqualTo(expectedTitle));
    }

    [Test]
    public async Task VisitTicketsPage()
    {
        await Page.GotoAsync("http://127.0.0.1:5500/Front/Pages/Login/index.html");
        await Page.GetByLabel("Username").ClickAsync();
        await Page.GetByLabel("Username").FillAsync("pwVisitTest");
        await Page.GetByLabel("Password").ClickAsync();
        await Page.GetByLabel("Password").FillAsync("pwVisitTest123@");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        await Page.GetByRole(AriaRole.Button, new() { Name = "Tickets" }).ClickAsync();
        await Page.WaitForLoadStateAsync();

        string expectedTitle = "Tickets";
        string actualTitle = await Page.TitleAsync();

        Assert.That(actualTitle, Is.EqualTo(expectedTitle));
    }

    [Test]
    public async Task VisitTasksPage()
    {
        await Page.GotoAsync("http://127.0.0.1:5500/Front/Pages/Login/index.html");
        await Page.GetByLabel("Username").ClickAsync();
        await Page.GetByLabel("Username").FillAsync("pwVisitTest");
        await Page.GetByLabel("Password").ClickAsync();
        await Page.GetByLabel("Password").FillAsync("pwVisitTest123@");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        await Page.GetByRole(AriaRole.Button, new() { Name = "Tasks" }).ClickAsync();
        await Page.WaitForLoadStateAsync();

        string expectedTitle = "Tasks";
        string actualTitle = await Page.TitleAsync();

        Assert.That(actualTitle, Is.EqualTo(expectedTitle));
    }

    [Test]
    public async Task VisitCreateTaskPage()
    {
        await Page.GotoAsync("http://127.0.0.1:5500/Front/Pages/Login/index.html");
        await Page.GetByLabel("Username").ClickAsync();
        await Page.GetByLabel("Username").FillAsync("pwVisitTest");
        await Page.GetByLabel("Password").ClickAsync();
        await Page.GetByLabel("Password").FillAsync("pwVisitTest123@");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        await Page.GetByRole(AriaRole.Button, new() { Name = "Create Task" }).ClickAsync();
        await Page.WaitForLoadStateAsync();

        string expectedTitle = "Create Task";
        string actualTitle = await Page.TitleAsync();

        Assert.That(actualTitle, Is.EqualTo(expectedTitle));
    }

    [Test]
    public async Task VisitMyProfilePage()
    {
        await Page.GotoAsync("http://127.0.0.1:5500/Front/Pages/Login/index.html");
        await Page.GetByLabel("Username").ClickAsync();
        await Page.GetByLabel("Username").FillAsync("pwVisitTest");
        await Page.GetByLabel("Password").ClickAsync();
        await Page.GetByLabel("Password").FillAsync("pwVisitTest123@");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Login" }).ClickAsync();
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        await Page.GetByRole(AriaRole.Button, new() { Name = "Profile" }).ClickAsync();
        await Page.WaitForLoadStateAsync();

        string expectedTitle = "My Profile";
        string actualTitle = await Page.TitleAsync();

        Assert.That(actualTitle, Is.EqualTo(expectedTitle));
    }
}
