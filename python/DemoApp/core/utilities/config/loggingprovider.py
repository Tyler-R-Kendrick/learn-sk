import logging

class LoggingProvider:
    @staticmethod
    def get_logger(name: str, logging_level) -> logging.Logger:
        logger = logging.getLogger(name)
        logger.setLevel(logging_level)

        # create console handler and set level to debug
        ch = logging.StreamHandler()  
        ch.setLevel(logging_level)
        
        # setup formatter
        formatter = logging.Formatter('%(asctime)s - %(name)s - %(levelname)s - %(message)s')
        ch.setFormatter(formatter)

        logger.addHandler(ch)
        
        return logger