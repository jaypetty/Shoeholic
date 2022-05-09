import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { useParams } from "react-router-dom";
import {Card,CardImg, CardText,CardTitle,Container,Button} from "reactstrap";
import { getShoesByCollectionId } from "../../modules/shoeManager";

const CollectionDetails = () => {
    const [shoes, setshoes] = useState([]);
    const {id} = useParams();

    const getShoeCollection = (id) => {
        getShoesByCollectionId(id).then((shoes) => setshoes(shoes))
    };

    useEffect(() => {
        getShoeCollection(id);
    }, [])
    if (shoes.length === 0) {
        return null
    };

    return ( 
        <>
        {shoes.map((shoe) => {
            
           return <Card>
            <CardTitle>{shoe.collection.name}</CardTitle>
            <CardText>
                <small>{shoe.brand.name}</small>
            </CardText>
            <CardText>
                <small>{shoe.name}</small>
            </CardText>
            <CardText>
                <small>{shoe.title}</small>
            </CardText>
            
        </Card> 
        })}
    </>
    );
}
export default CollectionDetails;