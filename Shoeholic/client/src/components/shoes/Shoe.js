import React from "react";
import { Card, CardBody, CardTitle, CardText} from "reactstrap";

const Shoe = ({shoe}) => {
    return (
        <Card>
            <CardBody>
                <CardTitle tag="h1">
                    {shoe.name}
                </CardTitle>
                <CardText>
                  <p>{shoe.brand}</p>  
                  <p>{shoe.title}</p>
                </CardText>
            </CardBody>
        </Card>
    );
};

export default Shoe;