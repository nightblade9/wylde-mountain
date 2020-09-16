import React from 'react'
import ReactDOM from 'react-dom';
import { MemoryRouter } from 'react-router-dom';
import { ExploreScene } from './ExploreScene';

beforeEach(() => {
  fetch.resetMocks();
});

it('renders current floor when user is authenticated', async () => {
  
  fetch
    .once(JSON.stringify({ "id": "bson ID", "emailAddress": "fake@fake.com", "dungeon": undefined }))
    .once(JSON.stringify(
      { "id": "dungeon ID", "currentFloor": {
        "floorNumber": 1, "events": [
          [{ "eventType": "Monster", "data": "Firetoad" }]
        ]
      }
    }));

  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <ExploreScene  />
    </MemoryRouter>, div);
  await new Promise(resolve => setTimeout(resolve, 1000));
  expect(div.textContent.includes("Unauthenticated"));
});

it('renders an empty placeholder if a list of events is empty', async () => {
  
  fetch
    .once(JSON.stringify({ "id": "bson ID", "emailAddress": "fake@fake.com", "dungeon": undefined }))
    .once(JSON.stringify(
      { "id": "dungeon ID", "currentFloor": {
        "floorNumber": 1, "events": [
          [{ "eventType": "Monster", "data": "Firetoad" }],
          [] // empty
        ]
      }
    }));

  const div = document.createElement('div');
  ReactDOM.render(
    <MemoryRouter>
      <ExploreScene  />
    </MemoryRouter>, div);
  await new Promise(resolve => setTimeout(resolve, 1000));
  expect(div.textContent.includes("(empty)"));
});