import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Homepage from "./HomePage";
import ShoeForm from "./shoes/ShoeForm";
import MyShoes from "./shoes/ShoeList";
import ShoeDetails from "./shoes/ShoeDetails";
import ShoeEdit from "./shoes/ShoeEdit";
import MyCollections from "./collections/CollectionList";
import CollectionDetails from "./collections/CollectionDetails";

export default function ApplicationViews({ isLoggedIn }) {
  return (
    <main>
      <Switch>

      <Route path="/" exact>
          {isLoggedIn ? <Homepage /> : <Redirect to="/login" />}
        </Route>

        <Route path="/myshoes" exact>
          {isLoggedIn ? <MyShoes /> : <Redirect to="/login" />}
        </Route>

        <Route path="/myshoes/details/:id(\d+)">
        {isLoggedIn ? <ShoeDetails /> : <Redirect to="/login" />}
        </Route>

        <Route path="/myshoes/newShoe" exact>
          {isLoggedIn ? <ShoeForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/myshoes/edit/:id(\d+)" exact>
          {isLoggedIn ? <ShoeEdit /> : <Redirect to="/login" />}
        </Route>

        <Route path="/mycollections" exact> 
          {isLoggedIn ? <MyCollections /> : <Redirect to="/login" />}
        </Route>

        <Route path="/mycollection/details/:id(\d+)">
        {isLoggedIn ? <CollectionDetails /> : <Redirect to="/login" />}
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