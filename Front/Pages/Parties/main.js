import { serverUrl } from "../../config.js";


export class PartiesPage {
    constructor() {

    }

    async draw() {
        const partiesRequest = await fetch (serverUrl + "/Party/parties");

        if (!partiesRequest.ok) return;

        const parties = await partiesRequest.json();
        console.log(parties);
    }
}

const partiesPage = new PartiesPage();
await partiesPage.draw();