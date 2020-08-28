import { globalSettings } from '../../App';
import JwtBody from '../../interfaces/Jwt';
import jwtDecode from 'jwt-decode';
import React from 'react';
import LocalizedStrings from 'react-localization';

interface TokenProps {
  token: string;
}

export function Identity(props: TokenProps) {
  const languageStrings = new LocalizedStrings({
    "en": require('~/../../resources/components/Authentication/Identity-en.json'),
    "ar": require('~/../../resources/components/Authentication/Identity-ar.json')
  });
  languageStrings.setLanguage(globalSettings.language);  
  
  return (
    <span>{props.token != null ? (jwtDecode(props.token) as JwtBody).email : languageStrings.unauthenticated}</span>
  )
}
