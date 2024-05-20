//import {render, screen} from '@testing-library/react'
import { expect, test } from 'vitest'
//import App from '../../src/App';
import { createAsyncThunk } from '@reduxjs/toolkit';
//import { create } from '../app/store.test';

/*test('Middleware must shows action rejection', async () => {
    const { next, invoke } = create();
    //const action = { type: 'rejected' };
    const action = createAsyncThunk("test/rejected", async (state: string, { rejectWithValue }) => {
        if (state != "accepted")
        return rejectWithValue('No user found');
      });
    invoke(action);
    expect(next).not.toHaveBeenCalledWith(action);
  });

it('App must be rendering', () => {
    const component = render(
        <App />,
    );

    expect(screen.).toMatchSnapshot();
  });*/

test('Value must be greater than', () => {
    expect(2).toBeGreaterThan(1);
  });