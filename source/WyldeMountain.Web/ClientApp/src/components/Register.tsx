import { globalSettings } from '../App';
import { Container, CssBaseline, Avatar, Typography, TextField, FormControlLabel, Checkbox, Button, Grid, Box, Link } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { LockOutlined as LockOutlinedIcon } from '@material-ui/icons'
import React, { ChangeEvent, useState } from 'react';
import LocalizedStrings from 'react-localization';
import { Link as RouterLink, useHistory } from 'react-router-dom';

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
    marginTop: theme.spacing(3),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
}));

const Register = (props:any) => {
  const classes = useStyles();

  // use state hooks
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [lastMessage, setMessage] = useState<string>("");

  // for routing
  const history = useHistory();

  const languageStrings = new LocalizedStrings({
    "en": require('~/../../resources/components/Register-en.json')//,
    //"ar": require('~/../../resources/components/Login-ar.json')
  });
  languageStrings.setLanguage(globalSettings.language);

  const onSumbit = (event: ChangeEvent<HTMLFormElement>) => {
    let state = {emailAddress: email, password: password};

    event.preventDefault();
  
    return fetch('api/register',
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
          throw new Error(languageStrings.registrationFailed);
        }
      })
      .then(data => {
        setMessage(languageStrings.registrationSucceeded)
        history.push("/login");
      })
      .catch(e => setMessage(languageStrings.registrationFailed));
  }

  return (
    <Container component="main" maxWidth="xs">
      <div className={classes.paper}>
        <Avatar className={classes.avatar}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Sign up
        </Typography>
        <div>{lastMessage}</div>
        <form className={classes.form} noValidate onSubmit={onSumbit}>
          <Grid container spacing={1}>
            <Grid item xs={12}>
              <TextField
                autoComplete="username"
                name="userName"
                variant="outlined"
                required
                fullWidth
                id="userName"
                label={languageStrings.userName}
                autoFocus
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                id="email"
                label={languageStrings.emailAddress}
                name="email"
                autoComplete="email"
                onChange={e => setEmail(e.target.value)}
              />
            </Grid>
            <Grid item xs={12}>
              <TextField
                variant="outlined"
                required
                fullWidth
                name="password"
                label={languageStrings.password}
                type="password"
                id="password"
                autoComplete="current-password"
                onChange={e => setPassword(e.target.value)}
              />
            </Grid>
          </Grid>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            color="primary"
            className={classes.submit}
          >
            {languageStrings.register}
          </Button>
          <Grid container justify="flex-end">
            <Grid item>
              <Link to="/login" variant="body2" component={RouterLink}>
                {languageStrings.signIn}
              </Link>
            </Grid>
          </Grid>
        </form>
      </div>
      {/* <Box mt={5}>
        <Copyright />
      </Box> */}
    </Container>
  );
}

export default Register;