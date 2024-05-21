import { fireEvent, getByText, render } from '@testing-library/react'
import { expect, test } from 'vitest';
import { LoginPage } from '../../src/pages/Login/LoginPage';
import { AppMock } from '../AppMock.tsx';
import userEvent from '@testing-library/user-event';

export const loginInput = () => {
    const {getByPlaceholderText, asFragment} = render(
        <LoginPage />,
    );

    const firstRender = asFragment();
    const user = userEvent.setup();

    fireEvent.change(getByPlaceholderText("Enter code"), {target: {input: "0"}});

    fireEvent.input(getByPlaceholderText("Enter code"));

    return firstRender;
}

export const loginSubmit = () => {
    const firstRender = loginInput();

    const user = userEvent.setup();

    const element = firstRender.children.item(0)?.parentElement;

    fireEvent.submit(getByText(element as HTMLElement, "Join"));
}