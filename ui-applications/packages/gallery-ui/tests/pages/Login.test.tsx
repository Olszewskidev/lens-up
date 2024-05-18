import { fireEvent, render } from '@testing-library/react'
import { expect, it } from '@jest/globals';
import { LoginPage } from '../../src/pages/Login/LoginPage';

it('Entered code must change in the form', () => {
    const {getByText, asFragment} = render(
        <LoginPage />,
    );

    const firstRender = asFragment();

    fireEvent.change(getByText("Join to your gallery"));
    
    expect(firstRender).toMatchSnapshot(asFragment());
  });

it('Form must be handled successfully', () => {
    const {getByText, asFragment} = render(
        <LoginPage />,
    );

    const firstRender = asFragment();

    fireEvent.submit(getByText("Join"));
    
    expect(firstRender).toMatchSnapshot(asFragment());
  });