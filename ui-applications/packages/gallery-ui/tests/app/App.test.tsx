import renderer from 'react-test-renderer';
import {expect, it} from '@jest/globals';
import App from '../src/App';
import { createAsyncThunk } from '@reduxjs/toolkit';
import { create } from './app/store.test';

it('Middleware must shows action rejection', () => {
    const { next, invoke } = create();
    //const action = { type: 'rejected' };
    const action = createAsyncThunk("test/rejected", async (state: string, { rejectWithValue }) => {
        if (state != "accepted")
        return rejectWithValue('No user found');
      });
    invoke(action);
    expect(next).not.toHaveBeenCalledWith(action);
  });

it('App must render', () => {
    const component = renderer.create(
        <App />,
    );
    let tree = component.toJSON();
    expect(tree).toMatchSnapshot();
  });