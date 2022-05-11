import React, { useEffect, useState} from "react";
import Shoe from "./Shoe";
import { getAllShoes } from "../../modules/shoeManager";
import { Button } from "reactstrap";
import { Link } from "react-router-dom";

const MyShoes = () => {
    const [shoes, setShoes] = useState([]);

    const getShoes = () => {
        getAllShoes().then(shoes => setShoes(shoes));
    };

    useEffect(() => {
        getShoes();
    }, []);

    return (
        <div className="container">
            <div className="row justify-content-center">
                {shoes.map((shoe) => (
                    <Shoe key={shoe.id} shoe={shoe}  />
                ))}
                <Button className="btn btn-primary" href="/myshoes/newShoe">Add a New Shoe</Button>
            </div>
        </div>
    );
};

export default MyShoes;