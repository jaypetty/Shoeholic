import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { useParams } from "react-router-dom";
import {Card,CardImg, CardText,CardTitle,Container,Button} from "reactstrap";
import { getShoeById } from "../../modules/shoeManager";
import { Link } from "react-router-dom";

const ShoeDetails = () => {
    const [shoe, setShoe] = useState();
    const {id} = useParams();

    const getShoe = (id) => {
        getShoeById(id).then((shoe) => setShoe(shoe));
    };

    useEffect(() => {
        getShoe(id);
    }, [])
    if (shoe == null){
        return null
    };

    return (
        <Container>
            <Card>
                <CardTitle>{shoe.name} by {shoe.brand.name}</CardTitle>
                <CardText>
                    <small>{shoe.releaseDate}</small>
                </CardText>
                <CardText>
                    <small>{shoe.retailPrice}</small>
                </CardText>
                <CardText>
                    <small>{shoe.purchaseDate}</small>
                </CardText>
                <CardText>
                    <small>{shoe.title}</small>
                </CardText>
                <CardText>
                    <small>{shoe.colorWay}</small>
                </CardText>
                <CardText>
                    <>{shoe.tags.map(tag =>{
                    return  tag.name
                    }).join(', ')}</>
                </CardText>
                <Link to={`/myshoes/edit/${id}`}>
            <Button>Edit</Button>
          </Link>
            </Card>
        </Container>
    );
};

export default ShoeDetails;