import React, { ChangeEvent, useState } from 'react';
import { Link as RouterLink, useHistory } from 'react-router-dom';
import { Container, Avatar, Typography, TextField, FormControlLabel, Checkbox, Button, Grid, makeStyles, Link } from '@material-ui/core';
import { LockOutlined as LockOutlinedIcon } from '@material-ui/icons'
import LocalizedStrings from 'react-localization';
import { globalSettings } from '../App';

const useStyles = makeStyles((theme) => ({
  paper: {
    marginTop: theme.spacing(8),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
  },
  form: {
    width: '100%', // Fix IE 11 issue.
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
}));

export function Login() {

  const languageStrings = new LocalizedStrings({
    "en": require('~/../../resources/components/Login-en.json'),
    "ar": require('~/../../resources/components/Login-ar.json')
  });

  languageStrings.setLanguage(globalSettings.language);
  
  const classes = useStyles();

  // use state hooks
  const [email, setEmail] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [lastMessage, setMessage] = useState<string>("");

  // for routing
  const history = useHistory();

  const onSumbit = (event: ChangeEvent<HTMLFormElement>) => {
    let state = { emailAddress: email, password: password };

    event.preventDefault();

    return fetch('api/login',
      {
        method: 'POST',
        headers: {
          'Accept': 'application/json',
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(state)
      })
      .then((response) => {
        if (response.ok) {
          return response.json();
        }
        else {
          throw new Error(languageStrings.loginFailed);
        }
      })
      .then(data => {
        setMessage(languageStrings.loginSucceeded);
        var token = data.token;
        localStorage.setItem("userInfo", token);
        history.push("/");
      })
      .catch(e => {
        // e is set to the generic message from a few lines up (.then(response => ...))
        setMessage(languageStrings.loginFailed)
      });
  }

  return (
    <Container component="main" maxWidth="xs">
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          {languageStrings.login}
      </Typography>
        <div>{lastMessage}</div>
        <form className={classes.form} noValidate onSubmit={onSumbit}>
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            id="email"
            label={languageStrings.emailAddress}
            name="email"
            autoComplete="email"
            autoFocus
            onChange={e => setEmail(e.target.value)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="password"
            label={languageStrings.password}
            type="password"
            id="password"
            autoComplete="current-password"
            onChange={e => setPassword(e.target.value)}
          />
          <FormControlLabel
            control={<Checkbox value="remember" color="primary" />}
            label={languageStrings.rememberMe}
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
          >
            {languageStrings.login}
        </Button>
          <Grid container>
            <Grid item xs>
              <Link variant="body2">
                {languageStrings.register} 
            </Link>
            </Grid>
            <Grid item>
              <Link to="/register" variant="body2" component={RouterLink}>
                {languageStrings.register}
            </Link>
            </Grid>
          </Grid>
        </form>
      </div>
    </Container>
  );
}