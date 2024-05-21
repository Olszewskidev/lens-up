import { fireEvent, render } from '@testing-library/react'
import { expect, test } from 'vitest';
import { LoginPage } from '../../src/pages/Login/LoginPage';
import { AppMock } from '../AppMock.tsx';

/**
* @vitest-environment jsdom
*/
test('Entered code must change in the form', async () => {
    AppMock();

    const {asFragment, getByText} = render(
        <LoginPage />,
    );

    const firstRender = asFragment();

    fireEvent.change(getByText("Join to your gallery"));
    
    expect(firstRender).toMatchSnapshot(asFragment());
  });

/**
* @vitest-environment jsdom
*/
test('Form must be handled successfully', () => {
    const {getByText, asFragment} = render(
        <LoginPage />,
    );

    const firstRender = asFragment();

    fireEvent.submit(getByText("Join"));
    
    expect(firstRender).toMatchSnapshot(asFragment());
  });

/**
* @vitest-environment jsdom
*/
test('Login must set gallery successfuly', () => {
  const {getByText, asFragment} = render(
      <LoginPage />,
  );

  const firstRender = asFragment();

  fireEvent.submit(getByText("Join"));
  
  expect(firstRender).toMatchSnapshot(asFragment());
});