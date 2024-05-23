import { fireEvent, getByText, render, screen } from '@testing-library/react'
import { expect, test } from 'vitest';
import { Provider } from 'react-redux'
import userEvent from '@testing-library/user-event';
import { LoginPage } from '../../src/pages/Login/LoginPage';
import { store } from '../../src/app/store/store.ts';
import { AppMock } from '../AppMock.tsx';
import { BrowserRouter } from 'react-router-dom';

export const loginInput = async () => {
    const {getByPlaceholderText, asFragment, baseElement} = render(
        <Provider store={store}>
          <BrowserRouter>
            <LoginPage />
          </BrowserRouter>
        </Provider>
    );

    fireEvent.change(screen.getByPlaceholderText("Enter code"), {target: {value: "0"}});

    return {asFragment, baseElement};
}

export const loginSubmit = async () => {
    const {asFragment, baseElement} = await loginInput();

    const firstRender = asFragment();

    const element = (firstRender).children[0];

    fireEvent.submit(getByText(element as HTMLElement, "Join"));
}

/*
"<div><img width=\"379\" height=\"147\" src=\"/@fs/C:/Users/safronov/source/lens-up/ui-applications/packages/shared-components/src/images/lens-up-logo.png\" alt=\"LensUp logo.\"><form class=\"px-7 py-4 bg-black rounded-lg leading-none border-double border-4 border-white shadow-lg max-w-sm mx-auto\"><div class=\"mb-5\"><div><label for=\":r0:\" class=\"block mb-2 text-sm font-medium text-gray-50 dark:text-white\">Join to your gallery:</label><input type=\"number\" id=\":r0:\" class=\"bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500\" placeholder=\"Enter code\" required=\"\" value=\"\"></div></div><div class=\"flex items-center justify-center\"><div class=\"text-center relative group\"><button type=\"submit\" class=\"text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-full text-sm w-full sm:w-auto px-5 py-2.5 text-center dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800 inline-flex items-center justify-center\">Join</button></div></div></form></div>"
*/