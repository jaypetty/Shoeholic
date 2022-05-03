import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Homepage from "./HomePage";
import ShoeForm from "./shoes/ShoeForm";

export default function ApplicationViews({ isLoggedIn }) {
  return (
    <main>
      <Switch>

      <Route path="/" exact>
          {isLoggedIn ? <Homepage /> : <Redirect to="/login" />}
        </Route>

        <Route path="/shoes/newShoe" exact>
          {isLoggedIn ? <ShoeForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/login">
          <Login />
        </Route>

        <Route path="/register">
          <Register />
        </Route>
        
      </Switch>
    </main>
  );
}