import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { Counter } from './components/Counter';
import { FetchData } from './components/FetchData';
import { Register } from './components/Register';
import { Login } from './components/Login';
import { CoreGame } from './components/CoreGame';
import LanguageSwitcher from './components/LanguageSwitcher';
import { GlobalSettings } from  './interfaces/IGlobalSettings';

export default class App extends Component
{
  render()
  {
    return (
      <Layout>
        <LanguageSwitcher />
        <Route exact path='/' component={Home} />
        <Route path='/register' component={Register} />
        <Route path='/login' component={Login} />
        <Route path='/counter' component={Counter} />
        <Route path='/fetch-data' component={FetchData} />
        <Route path='/core-game' component={CoreGame} />
      </Layout>
    );
  }
};

// global variable for I18N, herp derp
export var globalSettings:GlobalSettings =
{
  "language": "en"
}