<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="XDPTPM.client.thanks.index" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Cảm ơn</title>
    <style>
        @import url("https://fonts.googleapis.com/css2?family=Dela+Gothic+One&display=swap");

        * {
            box-sizing: border-box;
        }

        html,
        body {
            height: 100%;
        }

        body {
            --text-color: hsl(0 95% 60%);
            --bg-color: hsl(0 0% 100%);
            --bg-size: 200px;
            display: grid;
            place-items: center;
            place-content: center;
            grid-template-areas: "body";
            overflow: hidden;
            font-family: "Dela Gothic One", sans-serif;
            background-color: var(--bg-color);
        }

            body::before {
                --size: 150vmax;
                grid-area: body;
                content: "";
                inline-size: var(--size);
                block-size: var(--size);
                background-image: url("https://assets.codepen.io/225363/foot-pattern.svg");
                background-size: var(--bg-size);
                background-repeat: repeat;
                transform: rotate(45deg);
                opacity: 0.25;
                animation: bg 6s linear infinite;
            }

        @media (prefers-reduced-motion: reduce) {
            body::before {
                animation-duration: 0s;
            }
        }

        @keyframes bg {
            to {
                background-position: 0 calc(var(--bg-size) * -1);
            }
        }

        .text {
            grid-area: body;
            position: relative;
            display: flex;
            flex-direction: column;
            font-size: clamp(1rem, 10vmin, 6rem);
        }

        .heading span {
            display: block;
            text-transform: uppercase;
        }

            .heading span.filled {
                color: var(--text-color);
            }

            .heading span:not(.filled) {
                --stroke: min(0.25vmin, 2px) var(--text-color);
                color: var(--bg-color);
                -webkit-text-stroke: var(--stroke);
                text-stroke: var(--stroke);
            }

        .subheading {
            position: relative;
            margin-block-start: 1rem;
            margin-inline-start: auto;
            font-size: 0.428em;
            color: var(--text-color);
        }
    </style>
</head>

<body>
    <h1 class="text" aria-label="Thank you. Have a nice day!">
        <span class="heading" aria-hidden="true">
            <span>Thank you</span>
            <span>Thank you</span>
            <span class="filled">Thank you</span>
            <span>Thank you</span>
            <span>Thank you</span>
            <span>Thank you</span>
        </span>
        <span class="subheading" aria-hidden="true">Have a nice day</span>
    </h1>
</body>

</html>
