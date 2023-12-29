import { serverUrl } from "../../config.js";
import { Ticket } from "./ticket.js";

export class TicketsPage {
    constructor() {
        this.container = document.body.querySelector(".container");
        this.tickets = [];
    }

    async draw() {
        const ticketsRequest = await fetch (serverUrl + "/Party/attendingparties/" + localStorage.getItem("id"));

        if (!ticketsRequest.ok) {
            console.log("Greska");
            return;
        }

        this.tickets = await ticketsRequest.json();
        this.tickets.forEach(t => {
            const ticket = new Ticket(t);
            ticket.draw(this.container);
            console.log(ticket);
        });
    }
}

const ticketsPage = new TicketsPage();
await ticketsPage.draw();

