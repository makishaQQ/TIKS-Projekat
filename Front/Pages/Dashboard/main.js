import {baseUrl, serverUrl} from "../../config.js"

export class DashboardPage {
    constructor() {
        this.partiesButton = document.body.querySelector(".parties-button");
        this.myPartiesButton = document.body.querySelector(".my-parties-button");
        this.createPartyButton = document.body.querySelector(".create-party-button");
        this.ticketsButton = document.body.querySelector(".tickets-button");

        this.partiesButton.addEventListener("click", () => window.location.href = "http://localhost:5500" + "/Pages/Parties/index.html");
        this.myPartiesButton.addEventListener("click", async() => this.handleMyPartiesClick());
        this.createPartyButton.addEventListener("click", () => window.location.href = baseUrl + "/Pages/CreateParty/index.html");
        this.ticketsButton.addEventListener("click", () => window.location.href = baseUrl + "/Pages/Tickets/index.html");
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