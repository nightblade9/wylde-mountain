import React from 'react';
import { Redirect } from 'react-router-dom';
import { isUserAuthenticated } from '../../helpers/CurrentUser';

/// Annotate this in your render views to force redirecting to login if the user is not authenticated
export const RequireAuthentication = () =>
{
    if (!isUserAuthenticated())
    {
        return  <Redirect to="/login" />
    }
    return null;
}