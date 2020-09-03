import React, { Component } from 'react';
import { Route } from 'react-router';
import { useCookies } from 'react-cookie';

import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
import { FetchData } from './components/FetchData';
import Register from './components/Register';
import { Login } from './components/Login';
import { ExploreScene } from './components/scenes/ExploreScene';
import LanguageSwitcher from './components/LanguageSwitcher';
import BeginAdventure from './components/scenes/BeginAdventure';
import { Battle } from './components/scenes/Battle';

import { IGlobalSettings } from  './interfaces/IGlobalSettings';

import { COOKIE_NAME } from './Constants';

// global variable for I18N, herp derp
export var globalSettings:IGlobalSettings =
{
  "language": "en"
}

const App = (props:any) =>
{
  const [cookies] = useCookies([COOKIE_NAME]);

  if (cookies.language !== undefined)
  {
    globalSettings.language = cookies.language;
  }

  return (
    <Layout>
      <LanguageSwitcher />
      <Route exact path='/' component={Home} />
      <Route path='/register' component={Register} />
      <Route path='/login' component={Login} />
      <Route path='/counter' component={Counter} />
      <Route path='/fetch-data' component={FetchData} />
      <Route path='/begin-adventure' component={BeginAdventure} />
      <Route path='/explore' component={ExploreScene} />
      <Route path='/battle' component={Battle} />
    </Layout>
  );
};

export default App;