import { screen, render, fireEvent } from "@testing-library/react"
import { Options, userEvent } from "@testing-library/user-event"
import { expect, test, describe } from 'vitest';
import App from "../../src/App";
import { userEventApi } from "@testing-library/user-event/dist/cjs/setup/api.js";

/**
* @vitest-environment jsdom
*/
describe("Router test", () => {
    test("Check login route", async () => {
        render(<App />)

        const user = userEvent.setup();
        expect(global.window.location.pathname).equals("/");

        
    })
})