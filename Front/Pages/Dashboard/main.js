import {baseUrl, serverUrl} from "../../config.js"

export class DashboardPage {
    constructor() {
        this.partiesButton = document.body.querySelector(".parties-button");
        this.myPartiesButton = document.body.querySelector(".my-parties-button");
        this.createPartyButton = document.body.querySelector(".create-party-button");
        this.ticketsButton = document.body.querySelector(".tickets-button");
        this.tasksButton = document.body.querySelector(".tasks-button");
        this.createTaskButton = document.body.querySelector(".create-task-button");
        this.profileButton = document.body.querySelector(".profile-button");

        this.partiesButton.addEventListener("click", () => window.location.href = baseUrl + "/Pages/Parties/index.html");
        this.myPartiesButton.addEventListener("click", () => window.location.href = baseUrl + "/Pages/MyParties/index.html");
        this.createPartyButton.addEventListener("click", () => window.location.href = baseUrl + "/Pages/CreateParty/index.html");
        this.ticketsButton.addEventListener("click", () => window.location.href = baseUrl + "/Pages/Tickets/index.html");
        this.tasksButton.addEventListener("click", () => window.location.href = baseUrl + "/Pages/Tasks/index.html");
        this.createTaskButton.addEventListener("click", () => window.location.href = "/Pages/CreateTask/index.html");
        this.profileButton.addEventListener("click", () => window.location.href = baseUrl + "/Pages/UserProfile/index.html");
    }

    async handleMyPartiesClick() {
        const myPartiesRequest = await fetch (serverUrl + "/Party/myparties/" + localStorage.getItem("id"));

        if (!myPartiesRequest.ok) return;

        const parties = await myPartiesRequest.json();

        parties.forEach(p => {
            console.log(p);
        });
    }
}

const dashboardPage = new DashboardPage();