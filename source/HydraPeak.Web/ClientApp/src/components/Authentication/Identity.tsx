import React from 'react';
import JwtBody from '../../interfaces/Jwt';
import jwtDecode from 'jwt-decode';

interface TokenProps {
  token: string;
}

export function Identity(props: TokenProps) {
  return (
    <span>{props.token != null ? "Hi, " + (jwtDecode(props.token) as JwtBody).email : "Unauthenticated"}</span>
  )
}
